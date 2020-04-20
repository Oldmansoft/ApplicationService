using Oldmansoft.ApplicationService.MoneyBag.WebDefinition.MissionServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.WebDefinition.MissionServices
{
    class QueueConsumerFast : Util.LoopExecutor, IMission
    {
        public MissionDefaultValue Default
        {
            get
            {
                return new MissionDefaultValue(1);
            }
        }

        public string Name
        {
            get
            {
                return "队列消费-快速";
            }
        }

        protected override void Execute()
        {
            var application = new Application.InnerQueue();
            Callback(application);
        }

        private void Callback(Application.InnerQueue application)
        {
            if (RequestStop) return;
            DataDefinition.CallbackContent callbackContent;
            var category = DataDefinition.InnerQueueCategory.Callback;
            while (application.TryPeek(category, out callbackContent))
            {
                var postContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(callbackContent.Post), Encoding.UTF8);
                using (var httpClient = new HttpClient())
                {
                    httpClient.PostAsync(callbackContent.Uri, postContent);
                }
                application.TryDequeue(category);
                if (RequestStop) return;
            }
        }
    }
}
