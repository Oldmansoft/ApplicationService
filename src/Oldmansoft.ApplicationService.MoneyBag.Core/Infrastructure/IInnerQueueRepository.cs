using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Infrastructure
{
    public interface IInnerQueueRepository
    {
        void Enqueue<T>(DataDefinition.InnerQueueCategory category, T value);

        bool TryDequeue(DataDefinition.InnerQueueCategory category);

        bool TryDequeue<T>(DataDefinition.InnerQueueCategory category, out T value);

        bool TryPeek<T>(DataDefinition.InnerQueueCategory category, out T value);
    }
}
