using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.WebDefinition.MissionServices
{
    interface IMission
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 默认值
        /// </summary>
        Model.MissionDefaultValue Default { get; }

        /// <summary>
        /// 启动
        /// </summary>
        void Startup();

        /// <summary>
        /// 停止
        /// </summary>
        void Stop();

        /// <summary>
        /// 设置休眠
        /// </summary>
        /// <param name="seconds">秒数</param>
        void SetSleep(int seconds);

        /// <summary>
        /// 是否在运行中
        /// </summary>
        /// <returns></returns>
        bool IsRunning();

        /// <summary>
        /// 是否正在停止
        /// </summary>
        /// <returns></returns>
        bool IsStopping();

        /// <summary>
        /// 是否在执行业务
        /// </summary>
        /// <returns></returns>
        bool IsExecuting();
    }
}
