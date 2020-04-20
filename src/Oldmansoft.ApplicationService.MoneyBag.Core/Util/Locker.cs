using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Util
{
    public static class Locker
    {
        public static IDisposable Lock<T>(params T[] id)
            where T : struct
        {
            return new Locker<T>(id);
        }
    }

    class Locker<T> : IDisposable
        where T : struct
    {
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<T, object> Store = new System.Collections.Concurrent.ConcurrentDictionary<T, object>();

        private SortedSet<T> List;

        public Locker(params T[] id)
        {
            if (id == null || id.Length == 0) throw new ArgumentNullException();
            List = new SortedSet<T>();
            foreach (var item in id)
            {
                if (id.Count(o => o.Equals(item)) > 1) throw new ArgumentException("参数值不能重复");
                if (!Store.ContainsKey(item))
                {
                    Store.TryAdd(item, new object());
                }
                List.Add(item);
            }


            foreach (var item in List)
            {
                System.Threading.Monitor.Enter(Store[item]);
            }
        }

        public void Dispose()
        {
            foreach (var item in List.Reverse())
            {
                System.Threading.Monitor.Exit(Store[item]);
            }
        }
    }
}
