using Oldmansoft.ApplicationService.Sweepstake.WebDefinition.MissionServices;
using Oldmansoft.ApplicationService.Sweepstake.WebDefinition.MissionServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.WebDefinition
{
    /// <summary>
    /// 任务管理器
    /// </summary>
    public class MissionManager
    {
        /// <summary>
        /// 启动时间
        /// </summary>
        public static DateTime StartupTime { get; private set; }

        /// <summary>
        /// 实例
        /// </summary>
        public static readonly MissionManager Instance = new MissionManager();

        private IDictionary<string, IMission> Missions { get; set; }

        private MissionManager()
        {
            Missions = new Dictionary<string, IMission>();

            var source = new Application.Mission().List();
            var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (!type.GetInterfaces().Contains(typeof(IMission))) continue;
                var mission = ClassicDomain.ObjectCreator.CreateInstance(type) as IMission;
                mission.SetSleep(GetValue(mission, source.FirstOrDefault(o => o.Id == type.FullName)).Seconds);
                Missions.Add(type.FullName, mission);
            }
        }

        private MissionDefaultValue GetValue(IMission item, Data.MissionData data)
        {
            if (data != null)
            {
                return new MissionDefaultValue(data.Type, data.Seconds);
            }
            else
            {
                return item.Default;
            }
        }

        /// <summary>
        /// 系统启动
        /// </summary>
        public void SystemStartup()
        {
            StartupTime = DateTime.UtcNow;
            var source = new Application.Mission().List();
            foreach (var item in Missions)
            {
                if (GetValue(item.Value, source.FirstOrDefault(o => o.Id == item.Key)).Type != DataDefinition.StartupType.Automatic) continue;
                item.Value.Startup();
            }
        }

        /// <summary>
        /// 手动启动
        /// </summary>
        public void ManualStartup()
        {
            var source = new Application.Mission().List();
            foreach (var item in Missions)
            {
                if (GetValue(item.Value, source.FirstOrDefault(o => o.Id == item.Key)).Type == DataDefinition.StartupType.Disabled) continue;
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
            if (GetValue(Missions[id], new Application.Mission().Get(id)).Type == DataDefinition.StartupType.Disabled) return;
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
            var source = new Application.Mission().List();
            foreach (var item in Missions)
            {
                MissionModel model = GetModel(source.FirstOrDefault(o => o.Id == item.Key), item.Value);
                result.Add(model);
            }
            return result;
        }

        private MissionModel GetModel(Data.MissionData data, IMission item)
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

            var value = GetValue(item, data);
            model.Type = value.Type;
            model.Seconds = value.Seconds;
            return model;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MissionModel Get(string id)
        {
            if (!Missions.ContainsKey(id)) return null;
            return GetModel(new Application.Mission().Get(id), Missions[id]);
        }
    }
}
