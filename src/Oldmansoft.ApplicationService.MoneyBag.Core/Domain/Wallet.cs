using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Domain
{
    /// <summary>
    /// 钱包
    /// </summary>
    public class Wallet
    {
        /// <summary>
        /// 帐号序号
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// 分
        /// </summary>
        public int Cent { get; private set; }

        /// <summary>
        /// 最后交易时间
        /// </summary>
        public DateTime LastTime { get; private set; }

        /// <summary>
        /// 交易中
        /// </summary>
        public bool Trading { get; private set; }

        /// <summary>
        /// 锁定
        /// </summary>
        public DataDefinition.LockValue Locked { get; private set; }

        private Wallet() { }

        /// <summary>
        /// 创建钱包
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Wallet Create(Guid id)
        {
            var result = new Wallet();
            result.Id = id;
            result.Init();
            return result;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            Cent = 0;
            LastTime = DateTime.UtcNow;
            Trading = false;
        }

        /// <summary>
        /// 是否交易中
        /// </summary>
        /// <returns></returns>
        public bool IsTrading()
        {
            return Trading;
        }

        /// <summary>
        /// 开始交易
        /// </summary>
        public void StartTrade()
        {
            Trading = true;
        }

        /// <summary>
        /// 完成交易
        /// </summary>
        /// <param name="billing"></param>
        public void FinishTrade(Billing billing)
        {
            if (!Trading) return;

            Cent = billing.After;
            LastTime = billing.Created;
            Trading = false;
        }

        public void FinishTrade()
        {
            Trading = false;
        }

        /// <summary>
        /// 足够
        /// </summary>
        /// <param name="cent"></param>
        /// <returns></returns>
        public bool Enough(int cent)
        {
            if (cent <= 0) throw new ArgumentOutOfRangeException();
            return Cent >= cent;
        }

        /// <summary>
        /// 是否被其它人锁定
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool IsLockedByOther(Guid appId, string order)
        {
            if (!IsLocked()) return false;
            return !Locked.IsMime(appId, order);
        }

        /// <summary>
        /// 是否锁定
        /// </summary>
        /// <returns></returns>
        public bool IsLocked()
        {
            return Locked != null;
        }

        /// <summary>
        /// 锁住
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool Lock(Guid appId, string order)
        {
            if (IsLockedByOther(appId, order)) return false;
            Locked = DataDefinition.LockValue.Create(appId, order);
            return true;
        }

        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool Unlock(Guid appId, string order)
        {
            if (!IsLocked()) return true;
            if (!Locked.IsMime(appId, order)) return false;
            Locked = null;
            return true;
        }
    }
}
