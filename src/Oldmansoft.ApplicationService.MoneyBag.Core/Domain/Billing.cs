﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Domain
{
    /// <summary>
    /// 帐单
    /// </summary>
    public class Billing
    {
        /// <summary>
        /// 序号
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// 类型
        /// </summary>
        public DataDefinition.BillType Type { get; private set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public Guid AccountId { get; private set; }

        /// <summary>
        /// 客户端序号
        /// </summary>
        public DataDefinition.ClientContent Client { get; private set; }

        /// <summary>
        /// 转帐目标
        /// </summary>
        public DataDefinition.TransferContent Transfer { get; private set; }

        /// <summary>
        /// 交易值
        /// </summary>
        public int Trade { get; private set; }

        /// <summary>
        /// 交易后钱包的值
        /// </summary>
        public int After { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; private set; }

        /// <summary>
        /// 破碎的
        /// </summary>
        public bool Broken { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; private set; }

        private Billing() { }

        /// <summary>
        /// 创建转帐帐单
        /// </summary>
        /// <param name="value"></param>
        /// <param name="memo"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="sourceBillingId"></param>
        /// <param name="targetBillingId"></param>
        /// <param name="sourceBilling"></param>
        /// <param name="targetBilling"></param>
        public static void CreateTransfer(int value, string memo, Wallet source, Wallet target, long sourceBillingId, long targetBillingId, out Billing sourceBilling, out Billing targetBilling)
        {
            sourceBilling = new Billing();
            sourceBilling.Id = sourceBillingId;
            sourceBilling.Type = DataDefinition.BillType.Out;
            sourceBilling.Trade = value;
            sourceBilling.After = source.Value - value;
            sourceBilling.AccountId = source.Id;
            sourceBilling.Transfer = DataDefinition.TransferContent.Create(target.Id, targetBillingId);
            sourceBilling.Client = DataDefinition.ClientContent.CreateEmpty();
            sourceBilling.Memo = memo;
            sourceBilling.Created = DateTime.UtcNow;

            targetBilling = new Billing();
            targetBilling.Id = targetBillingId;
            targetBilling.Type = DataDefinition.BillType.In;
            targetBilling.Trade = value;
            targetBilling.After = target.Value + value;
            targetBilling.AccountId = target.Id;
            targetBilling.Transfer = DataDefinition.TransferContent.Create(source.Id, sourceBillingId);
            targetBilling.Client = DataDefinition.ClientContent.CreateEmpty();
            targetBilling.Memo = memo;
            targetBilling.Created = DateTime.UtcNow;
        }

        /// <summary>
        /// 创建消费帐单
        /// </summary>
        /// <param name="value"></param>
        /// <param name="memo"></param>
        /// <param name="wallet"></param>
        /// <param name="billingId"></param>
        /// <param name="clientAppId"></param>
        /// <param name="clientOrder"></param>
        /// <returns></returns>
        public static Billing CreateExpend(int value, string memo, Wallet wallet, long billingId, Guid clientAppId, string clientOrder)
        {
            var billing = new Billing();
            billing.Id = billingId;
            billing.Type = DataDefinition.BillType.Expend;
            billing.Trade = value;
            billing.After = wallet.Value - value;
            billing.AccountId = wallet.Id;
            billing.Client = DataDefinition.ClientContent.Create(clientAppId, clientOrder);
            billing.Memo = memo;
            billing.Created = DateTime.UtcNow;
            return billing;
        }

        /// <summary>
        /// 创建充值帐单
        /// </summary>
        /// <param name="value"></param>
        /// <param name="memo"></param>
        /// <param name="wallet"></param>
        /// <param name="billingId"></param>
        /// <param name="clientAppId"></param>
        /// <param name="clientOrder"></param>
        /// <returns></returns>
        public static Billing CreateRecharge(int value, string memo, Wallet wallet, long billingId, Guid clientAppId, string clientOrder)
        {
            var billing = new Billing();
            billing.Id = billingId;
            billing.Type = DataDefinition.BillType.Recharge;
            billing.Trade = value;
            billing.After = wallet.Value + value;
            billing.AccountId = wallet.Id;
            billing.Client = DataDefinition.ClientContent.Create(clientAppId, clientOrder);
            billing.Memo = memo;
            billing.Created = DateTime.UtcNow;
            return billing;
        }

        /// <summary>
        /// 是否为有效的转帐
        /// </summary>
        /// <param name="billing"></param>
        /// <returns></returns>
        public bool IsValidTransfer(Billing billing)
        {
            if (billing.Transfer == null) return false;
            if (billing.Transfer.BillingId != Id) return false;
            if (billing.Transfer.AccountId != AccountId) return false;
            if (Transfer == null) return false;
            if (billing.Id != Transfer.BillingId) return false;
            if (billing.AccountId != Transfer.AccountId) return false;
            if (billing.Trade != Trade) return false;
            if (billing.Type == DataDefinition.BillType.In && Type == DataDefinition.BillType.Out) return true;
            if (billing.Type == DataDefinition.BillType.Out && Type == DataDefinition.BillType.In) return true;
            return false;
        }

        /// <summary>
        /// 打碎
        /// </summary>
        public void Break()
        {
            Broken = true;
        }
    }
}
