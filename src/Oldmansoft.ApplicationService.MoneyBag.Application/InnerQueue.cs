using Oldmansoft.ApplicationService.MoneyBag.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Application
{
    public class InnerQueue : ApplicationBase
    {
        private IInnerQueueRepository Repository { get; set; }

        public InnerQueue()
        {
            Repository = Factory.CreateInnerQueueRepository();
        }

        public void Enqueue<T>(DataDefinition.InnerQueueCategory category, T value)
        {
            Repository.Enqueue(category, value);
        }

        public bool TryDequeue(DataDefinition.InnerQueueCategory category)
        {
            return Repository.TryDequeue(category);
        }

        public bool TryDequeue<T>(DataDefinition.InnerQueueCategory category, out T value)
        {
            return Repository.TryDequeue(category, out value);
        }

        public bool TryPeek<T>(DataDefinition.InnerQueueCategory category, out T value)
        {
            return Repository.TryPeek(category, out value);
        }
    }
}
