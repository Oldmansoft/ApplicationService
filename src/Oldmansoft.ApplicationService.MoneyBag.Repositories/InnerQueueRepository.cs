using Oldmansoft.ApplicationService.MoneyBag.Domain;
using Oldmansoft.ApplicationService.MoneyBag.Infrastructure;
using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Repositories
{
    class InnerQueueRepository : ClassicDomain.Driver.Redis.Repository<InnerQueue, Guid, RedisMapping>, IInnerQueueRepository
    {
        public InnerQueueRepository(UnitOfWork uow)
            : base(uow)
        { }

        public void Enqueue<T>(DataDefinition.InnerQueueCategory category, T value)
        {
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(value);

            Execute<bool>((database) => {
                database.ListRightPush(string.Format("Queue.{0}", category.ToString()), content, StackExchange.Redis.When.Always, StackExchange.Redis.CommandFlags.FireAndForget);
                return true;
            });
        }

        public bool TryDequeue(DataDefinition.InnerQueueCategory category)
        {
            var content = Execute<string>((database) => {
                return database.ListLeftPop(string.Format("Queue.{0}", category.ToString()));
            });
            return content != null;
        }

        public bool TryDequeue<T>(DataDefinition.InnerQueueCategory category, out T value)
        {
            var content = Execute<string>((database) => {
                return database.ListLeftPop(string.Format("Queue.{0}", category.ToString()));
            });
            if (content == null)
            {
                value = default(T);
                return false;
            }

            value = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
            return true;
        }

        public bool TryPeek<T>(DataDefinition.InnerQueueCategory category, out T value)
        {
            var content = Execute<string>((database) => {
                return database.ListGetByIndex(string.Format("Queue.{0}", category.ToString()), 0);
            });
            if (content == null)
            {
                value = default(T);
                return false;
            }

            value = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
            return true;
        }
    }
}
