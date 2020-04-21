using Oldmansoft.ApplicationService.MoneyBag.Infrastructure;
using Oldmansoft.ApplicationService.MoneyBag.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Services
{
    public class TradeService
    {
        private static IdLongGenerator Generator;

        private static object GeneratorLocker = new object();

        private IRepositoryFactory Factory;

        public TradeService(IRepositoryFactory factory)
        {
            Factory = factory;

            if (Generator == null)
            {
                lock (GeneratorLocker)
                {
                    if (Generator == null)
                    {
                        var start = Factory.CreateBillingRepository().GetLashId() + 1;
                        Generator = new IdLongGenerator(1, start);
                    }
                }
            }
        }

        /// <summary>
        /// 加载钱包
        /// </summary>
        /// <param name="walletRepository"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private Domain.Wallet LoadWallet(IWalletRepository walletRepository, Guid id)
        {
            var wallet = walletRepository.Get(id);
            if (wallet == null)
            {
                wallet = Domain.Wallet.Create(id);
                walletRepository.Add(wallet);
                try
                {
                    Factory.GetUnitOfWork().Commit();
                }
                catch (ClassicDomain.UniqueException)
                {
                    wallet = walletRepository.Get(id);
                }
            }
            else if (wallet.IsTrading())
            {
                var billingRepository = Factory.CreateBillingRepository();
                var billing = GetLastValidBilling(billingRepository, id);
                if (billing == null)
                {
                    wallet.Init();
                }
                else
                {
                    wallet.FinishTrade(billing);
                }
                walletRepository.Replace(wallet);
                Factory.GetUnitOfWork().Commit();
            }
            return wallet;
        }

        private Domain.Billing GetLastValidBilling(IBillingRepository billingRepository, Guid walletId, long fromBadBillingPosition = 0)
        {
            var currentId = fromBadBillingPosition;
            while (true)
            {
                var billing = billingRepository.GetLastBefore(walletId, currentId);
                if (billing == null) return null;

                if (billing.Type == DataDefinition.BillType.Expend)
                {
                    return billing;
                }
                if (billing.Type == DataDefinition.BillType.Recharge)
                {
                    return billing;
                }

                var switchBilling = billingRepository.GetSwitchTarget(billing.Id);
                if (switchBilling != null && switchBilling.IsValidTransfer(billing))
                {
                    return billing;
                }

                billing.Break();
                billingRepository.Replace(billing);
                currentId = billing.Id;
            }
        }

        /// <summary>
        /// 获取钱包
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Domain.Wallet GetWallet(Guid accountId)
        {
            var walletRepository = Factory.CreateWalletRepository();
            var wallet = walletRepository.Get(accountId);
            if (wallet == null || wallet.IsTrading())
            {
                using (Locker.Lock(accountId))
                {
                    return LoadWallet(walletRepository, accountId);
                }
            }
            return wallet;
        }

        /// <summary>
        /// 锁定钱包
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="appId"></param>
        /// <param name="order"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataDefinition.TradeState LockWallet(Guid accountId, Guid appId, string order, int value)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException("value");
            var walletRepository = Factory.CreateWalletRepository();
            using (Locker.Lock(accountId))
            {
                var wallet = LoadWallet(walletRepository, accountId);

                if (!wallet.Enough(value)) return DataDefinition.TradeState.InsufficientBalance;
                if (!wallet.Lock(appId, order)) return DataDefinition.TradeState.Locked;
                walletRepository.Replace(wallet);
                Factory.GetUnitOfWork().Commit();
                return DataDefinition.TradeState.Success;
            }
        }

        /// <summary>
        /// 解锁钱包
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="appId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool UnlockWallet(Guid accountId, Guid appId, string order)
        {
            var walletRepository = Factory.CreateWalletRepository();
            using (Locker.Lock(accountId))
            {
                var wallet = LoadWallet(walletRepository, accountId);

                if (!wallet.Unlock(appId, order)) return false;
                walletRepository.Replace(wallet);
                Factory.GetUnitOfWork().Commit();
                return true;
            }
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="clientAppId"></param>
        /// <param name="clientOrder"></param>
        /// <param name="value"></param>
        /// <param name="memo"></param>
        /// <param name="callback"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public bool Recharge(Guid accountId, Guid clientAppId, string clientOrder, int value, string memo, string callback, out long transactionId)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException("value");

            Domain.Billing billing;
            var walletRepository = Factory.CreateWalletRepository();
            using (Locker.Lock(accountId))
            {
                var wallet = LoadWallet(walletRepository, accountId);

                wallet.StartTrade();
                walletRepository.Replace(wallet);
                Factory.GetUnitOfWork().Commit();

                billing = Domain.Billing.CreateRecharge(value, memo, wallet, Generator.Next(), clientAppId, clientOrder);
                Factory.CreateBillingRepository().Add(billing);
                try
                {
                    Factory.GetUnitOfWork().Commit();
                }
                catch (ClassicDomain.UniqueException)
                {
                    wallet.FinishTrade();
                    walletRepository.Replace(wallet);
                    Factory.GetUnitOfWork().Commit();
                    transactionId = 0;
                    return false;
                }

                wallet.FinishTrade(billing);
                walletRepository.Replace(wallet);
                Factory.GetUnitOfWork().Commit();
            }
            if (!string.IsNullOrWhiteSpace(callback))
            {
                var data = Newtonsoft.Json.JsonConvert.SerializeObject(new DataDefinition.CallbackPostContent(accountId, clientAppId, clientOrder, billing.Id, value));
                Factory.CreateInnerQueueRepository().Enqueue(DataDefinition.InnerQueueCategory.Callback, new DataDefinition.CallbackContent() { Uri = callback, Post = data });
            }
            transactionId = billing.Id;
            return true;
        }

        /// <summary>
        /// 消费
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="clientAppId"></param>
        /// <param name="clientOrder"></param>
        /// <param name="value"></param>
        /// <param name="memo"></param>
        /// <param name="callback"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public DataDefinition.TradeState Expend(Guid accountId, Guid clientAppId, string clientOrder, int value, string memo, string callback, out long transactionId)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException("value");
            transactionId = 0;
            Domain.Billing billing;
            var walletRepository = Factory.CreateWalletRepository();
            using (Locker.Lock(accountId))
            {
                var wallet = LoadWallet(walletRepository, accountId);
                if (!wallet.Enough(value))
                {
                    return DataDefinition.TradeState.InsufficientBalance;
                }
                else if (wallet.IsLockedByOther(clientAppId, clientOrder))
                {
                    return DataDefinition.TradeState.Locked;
                }

                wallet.StartTrade();
                walletRepository.Replace(wallet);
                Factory.GetUnitOfWork().Commit();

                billing = Domain.Billing.CreateExpend(value, memo, wallet, Generator.Next(), clientAppId, clientOrder);
                Factory.CreateBillingRepository().Add(billing);
                try
                {
                    Factory.GetUnitOfWork().Commit();
                }
                catch (ClassicDomain.UniqueException)
                {
                    wallet.FinishTrade();
                    walletRepository.Replace(wallet);
                    Factory.GetUnitOfWork().Commit();
                    return DataDefinition.TradeState.ExistClientOrder;
                }

                wallet.FinishTrade(billing);
                walletRepository.Replace(wallet);
                Factory.GetUnitOfWork().Commit();
            }
            if (!string.IsNullOrWhiteSpace(callback))
            {
                var data = Newtonsoft.Json.JsonConvert.SerializeObject(new DataDefinition.CallbackPostContent(accountId, clientAppId, clientOrder, billing.Id, value));
                Factory.CreateInnerQueueRepository().Enqueue(DataDefinition.InnerQueueCategory.Callback, new DataDefinition.CallbackContent() { Uri = callback, Post = data });
            }
            transactionId = billing.Id;
            return DataDefinition.TradeState.Success;
        }

        /// <summary>
        /// 扣款
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="clientAppId"></param>
        /// <param name="clientOrder"></param>
        /// <param name="value"></param>
        /// <param name="memo"></param>
        /// <param name="callback"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public DataDefinition.TradeState Chargeback(Guid accountId, Guid clientAppId, string clientOrder, int value, string memo, string callback, out long transactionId)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException("value");
            transactionId = 0;
            Domain.Billing billing;
            var walletRepository = Factory.CreateWalletRepository();
            using (Locker.Lock(accountId))
            {
                var wallet = LoadWallet(walletRepository, accountId);
                if (wallet.IsLockedByOther(clientAppId, clientOrder))
                {
                    return DataDefinition.TradeState.Locked;
                }

                wallet.StartTrade();
                walletRepository.Replace(wallet);
                Factory.GetUnitOfWork().Commit();

                billing = Domain.Billing.CreateExpend(value, memo, wallet, Generator.Next(), clientAppId, clientOrder);
                Factory.CreateBillingRepository().Add(billing);
                try
                {
                    Factory.GetUnitOfWork().Commit();
                }
                catch (ClassicDomain.UniqueException)
                {
                    wallet.FinishTrade();
                    walletRepository.Replace(wallet);
                    Factory.GetUnitOfWork().Commit();
                    return DataDefinition.TradeState.ExistClientOrder;
                }

                wallet.FinishTrade(billing);
                walletRepository.Replace(wallet);
                Factory.GetUnitOfWork().Commit();
            }
            if (!string.IsNullOrWhiteSpace(callback))
            {
                var data = Newtonsoft.Json.JsonConvert.SerializeObject(new DataDefinition.CallbackPostContent(accountId, clientAppId, clientOrder, billing.Id, value));
                Factory.CreateInnerQueueRepository().Enqueue(DataDefinition.InnerQueueCategory.Callback, new DataDefinition.CallbackContent() { Uri = callback, Post = data });
            }
            transactionId = billing.Id;
            return DataDefinition.TradeState.Success;
        }

        /// <summary>
        /// 转帐
        /// </summary>
        /// <param name="sourceAccountId"></param>
        /// <param name="targetAccountId"></param>
        /// <param name="value"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public DataDefinition.TradeState Transfer(Guid sourceAccountId, Guid targetAccountId, int value, string memo)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException("value");
            if (sourceAccountId == targetAccountId) throw new ArgumentException("sourceAccountId = targetAccountId");

            var walletRepository = Factory.CreateWalletRepository();
            var billingRepository = Factory.CreateBillingRepository();
            using (Locker.Lock(sourceAccountId, targetAccountId))
            {
                var source = LoadWallet(walletRepository, sourceAccountId);
                var target = LoadWallet(walletRepository, targetAccountId);

                if (!source.Enough(value))
                {
                    return DataDefinition.TradeState.InsufficientBalance;
                }
                else if (source.IsLocked())
                {
                    return DataDefinition.TradeState.Locked;
                }

                source.StartTrade();
                target.StartTrade();
                walletRepository.Replace(source);
                walletRepository.Replace(target);
                Factory.GetUnitOfWork().Commit();

                var sourceBillingId = Generator.Next();
                var targetBillingId = Generator.Next();

                Domain.Billing sourceBilling;
                Domain.Billing targetBilling;
                Domain.Billing.CreateTransfer(value, memo, source, target, sourceBillingId, targetBillingId, out sourceBilling, out targetBilling);
                billingRepository.Add(sourceBilling);
                billingRepository.Add(targetBilling);
                Factory.GetUnitOfWork().Commit();

                source.FinishTrade(sourceBilling);
                target.FinishTrade(targetBilling);
                walletRepository.Replace(source);
                walletRepository.Replace(target);

                Factory.GetUnitOfWork().Commit();
                return DataDefinition.TradeState.Success;
            }
        }
    }
}
