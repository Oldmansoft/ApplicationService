using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.WebDefinition.Util
{
    /// <summary>
    /// 循环执行器
    /// </summary>
    public abstract class LoopExecutor : DataDefinition.IExecutor
    {
        private object Locker = new object();

        private Thread Core { get; set; }

        /// <summary>
        /// 休眠秒数
        /// </summary>
        private int SleepSeconds { get; set; }

        /// <summary>
        /// 线程在运行
        /// </summary>
        private bool ThreadIsExecuting { get; set; }

        /// <summary>
        /// 内部业务在执行
        /// </summary>
        private bool InnerExecuting { get; set; }

        /// <summary>
        /// 请求停止
        /// </summary>
        public bool RequestStop { get; private set; }

        /// <summary>
        /// 创建循环执行器
        /// </summary>
        public LoopExecutor()
        {
            SleepSeconds = 1;
            ThreadIsExecuting = false;
            InnerExecuting = false;
        }

        private void LoopExecute()
        {
            while (!RequestStop)
            {
                ThreadSleep(SleepSeconds);
                InnerExecuting = true;
                try
                {
                    Execute();
                }
                catch (Exception)
                {
                    //Logger.Error(ex.Message, ex);
                }
                InnerExecuting = false;
            }
            ThreadIsExecuting = false;
        }

        /// <summary>
        /// 线程睡眠
        /// </summary>
        /// <param name="seconds"></param>
        protected void ThreadSleep(int seconds)
        {
            for (var i = 0; i < seconds; i++)
            {
                Thread.Sleep(1000);
                if (RequestStop) break;
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected abstract void Execute();

        /// <summary>
        /// 开始
        /// </summary>
        public void Startup()
        {
            lock (Locker)
            {
                if (ThreadIsExecuting) return;
                ThreadIsExecuting = true;
            }
            RequestStop = false;
            Core = new Thread(LoopExecute);
            Core.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            if (!ThreadIsExecuting) return;
            RequestStop = true;
        }

        /// <summary>
        /// 设置休眠
        /// </summary>
        /// <param name="seconds">秒数</param>
        public void SetSleep(int seconds)
        {
            SleepSeconds = seconds;
        }

        /// <summary>
        /// 是否在运行中
        /// </summary>
        /// <returns></returns>
        public bool IsRunning()
        {
            return ThreadIsExecuting;
        }

        /// <summary>
        /// 是否正在停止
        /// </summary>
        /// <returns></returns>
        public bool IsStopping()
        {
            return IsRunning() && RequestStop;
        }

        /// <summary>
        /// 是否在执行业务
        /// </summary>
        /// <returns></returns>
        public bool IsExecuting()
        {
            return InnerExecuting;
        }
    }
}
