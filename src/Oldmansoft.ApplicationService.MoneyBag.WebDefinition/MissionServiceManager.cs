using Oldmansoft.ApplicationService.MoneyBag.WebDefinition.MissionServices;
using Oldmansoft.ApplicationService.MoneyBag.WebDefinition.MissionServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.WebDefinition
{
    /// <summary>
    /// 任务服务管理器
    /// </summary>
    public class MissionServiceManager
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static readonly MissionServiceManager Instance = new MissionServiceManager();

        /// <summary>
        /// 启动时间
        /// </summary>
        public DateTime StartupTime { get; private set; }

        private IDictionary<string, IMission> Missions { get; set; }

        private MissionServiceManager()
        {
            Missions = new Dictionary<string, IMission>();

            var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (!type.GetInterfaces().Contains(typeof(IMission))) continue;
                var mission = Oldmansoft.ClassicDomain.ObjectCreator.CreateInstance(type) as IMission;
                mission.SetSleep(mission.Default.Seconds);
                Missions.Add(type.FullName, mission);
            }
        }

        /// <summary>
        /// 系统启动
        /// </summary>
        public void SystemStartup()
        {
            StartupTime = DateTime.UtcNow;
            foreach (var item in Missions)
            {
                item.Value.Startup();
            }
        }

        /// <summary>
        /// 手动启动
        /// </summary>
        public void ManualStartup()
        {
            foreach (var item in Missions)
            {
                item.Value.Startup();
            }
        }

        /// <summary>
        /// 手动启动
        /// </summary>
        /// <param name="id"></param>
        public void ManualStartup(string id)
        {
            if (!Missions.ContainsKey(id)) return;
            Missions[id].Startup();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            foreach (var mission in Missions.Values)
            {
                mission.Stop();
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="id"></param>
        public void Stop(string id)
        {
            if (!Missions.ContainsKey(id)) return;
            Missions[id].Stop();
        }

        /// <summary>
        /// 设置休眠秒数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seconds"></param>
        public void SetSleep(string id, int seconds)
        {
            if (!Missions.ContainsKey(id)) return;
            Missions[id].SetSleep(seconds);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IList<MissionModel> List()
        {
            var result = new List<MissionModel>();
            foreach (var item in Missions)
            {
                MissionModel model = GetModel(item.Value);
                result.Add(model);
            }
            return result;
        }

        private MissionModel GetModel(IMission item)
        {
            var model = new MissionModel();
            model.Id = item.GetType().FullName;
            model.Name = item.Name;
            if (item.IsStopping())
            {
                model.State = MissionRunState.Stopping;
            }
            else if (item.IsRunning())
            {
                model.State = MissionRunState.Running;
            }
            else
            {
                model.State = MissionRunState.Stoped;
            }
            model.Inner = item.IsExecuting() ? MissionExecuteState.Busy : MissionExecuteState.Idle;

            var value = item.Default;
            model.Seconds = value.Seconds;
            return model;
        }
    }
}
