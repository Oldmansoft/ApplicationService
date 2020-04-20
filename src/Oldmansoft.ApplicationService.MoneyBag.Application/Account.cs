﻿using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Application
{
    public class Account : ApplicationBase
    {
        public int GetWallet(Guid id)
        {
            return new Services.TradeService(Factory).GetWallet(id).Cent;
        }

        public DataDefinition.LockValue GetLocked(Guid id)
        {
            return new Services.TradeService(Factory).GetWallet(id).Locked;
        }

        /// <summary>
        /// 锁定钱包
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="appId"></param>
        /// <param name="orderId"></param>
        /// <param name="cent"></param>
        /// <returns></returns>
        public DataDefinition.TradeState LockWallet(Guid accountId, Guid appId, string orderId, int cent)
        {
            return new Services.TradeService(Factory).LockWallet(accountId, appId, orderId, cent);
        }

        /// <summary>
        /// 解锁钱包
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="appId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool UnlockWallet(Guid accountId, Guid appId, string orderId)
        {
            return new Services.TradeService(Factory).UnlockWallet(accountId, appId, orderId);
        }

        public List<Data.BillingData> PagingBilling(Guid accountId, int index, int size, out int totalCount)
        {
            var list = Factory.CreateBillingRepository().Paging(accountId).Size(size).ToList(index, out totalCount);
            return list.MapTo(new List<Data.BillingData>());
        }

        public IList<Data.BillingData> ListBilling(Guid accountId, int skip, int count)
        {
            var list = Factory.CreateBillingRepository().List(accountId, skip, count);
            return list.MapTo(new List<Data.BillingData>());
        }

        public IList<Data.BillingData> ListBilling(Guid accountId, DateTime start, DateTime finish)
        {
            var list = Factory.CreateBillingRepository().List(accountId, start, finish);
            return list.MapTo(new List<Data.BillingData>());
        }

        public List<Data.BillingData> PagingBilling(int index, int size, out int totalCount)
        {
            var list = Factory.CreateBillingRepository().Paging().Size(size).ToList(index, out totalCount);
            return list.MapTo(new List<Data.BillingData>());
        }

        public long All()
        {
            return Factory.CreateWalletRepository().All();
        }

        public Data.BillingData GetByClient(Guid clientAppId, string clientOrdreId)
        {
            var domain = Factory.CreateBillingRepository().GetByClient(clientAppId, clientOrdreId);
            return domain.MapTo(new Data.BillingData());
        }

        public Data.BillingData GetByTransaction(long id)
        {
            var domain = Factory.CreateBillingRepository().Get(id);
            return domain.MapTo(new Data.BillingData());
        }
    }
}