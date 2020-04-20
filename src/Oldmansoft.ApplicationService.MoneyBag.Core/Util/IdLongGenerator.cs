using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Util
{
    /// <summary>
    /// 序号生成器
    /// </summary>
    public class IdLongGenerator
    {
        /// <summary>
        /// 当前值
        /// </summary>
        private long Store { get; set; }

        /// <summary>
        /// 初始值
        /// </summary>
        private long Init { get; set; }

        /// <summary>
        /// 开始值
        /// </summary>
        private long Start { get; set; }

        /// <summary>
        /// 上限值
        /// </summary>
        private long Max { get; set; }

        private object Locker = new object();

        /// <summary>
        /// 创建序号生成器
        /// </summary>
        public IdLongGenerator() : this(1, 1, long.MaxValue) { }

        /// <summary>
        /// 创建序号生成器
        /// </summary>
        /// <param name="init">初始值</param>
        public IdLongGenerator(long init) : this(init, init, long.MaxValue) { }

        /// <summary>
        /// 创建序号生成器
        /// </summary>
        /// <param name="init">初始值</param>
        /// <param name="start">开始值</param>
        public IdLongGenerator(long init, long start) : this(init, start, long.MaxValue) { }

        /// <summary>
        /// 创建序号生成器
        /// </summary>
        /// <param name="init">初始值</param>
        /// <param name="start">开始值</param>
        /// <param name="max">上限值</param>
        public IdLongGenerator(long init, long start, long max)
        {
            Init = init;
            if (start < init)
            {
                Start = init;
            }
            else
            {
                Start = start;
            }
            Store = Start - 1;
            Max = max;
        }

        /// <summary>
        /// 获取下一个值
        /// </summary>
        /// <returns></returns>
        public long Next()
        {
            lock (Locker)
            {
                if (Store == Max)
                {
                    Store = Init - 1;
                }
                else
                {
                    Store++;
                }
            }

            return Store;
        }
    }
}
