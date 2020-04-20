using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Domain
{
    /// <summary>
    /// 活动
    /// </summary>
    public class SweepstakeTheme
    {
        public Guid Id { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 中奖者
        /// </summary>
        public List<List<Guid>> Winner { get; private set; }

        /// <summary>
        /// 预订设定
        /// </summary>
        public DataDefinition.SweepstakeThemeBook Book { get; private set; }
        
        /// <summary>
        /// 状态
        /// </summary>
        public DataDefinition.SweepstakeThemeState State { get; private set; }

        /// <summary>
        /// 创建
        /// </summary>
        public DateTime Created { get; private set; }

        private SweepstakeTheme()
        {
            Winner = new List<List<Guid>>();
            State = DataDefinition.SweepstakeThemeState.Created;
            Created = DateTime.UtcNow;
        }

        public static SweepstakeTheme Create(string name, DateTime start, DateTime finish, int floor, int ceiling, List<int> definition)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            
            var result = new SweepstakeTheme();
            result.Name = name;
            result.Book = DataDefinition.SweepstakeThemeBook.Create(start, finish, floor, ceiling, definition);
            return result;
        }

        public void Change(string name, DateTime start, DateTime finish, int floor, int ceiling, List<int> definition)
        {
            if (State > DataDefinition.SweepstakeThemeState.Created) throw new DomainException("非创建状态不能修改");
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            Name = name;
            Book.Change(start, finish, floor, ceiling, definition);
        }

        public void Win(IList<Guid> members)
        {
            if (members == null) throw new ArgumentNullException("members");
            if (State != DataDefinition.SweepstakeThemeState.Finished) return;
            State = DataDefinition.SweepstakeThemeState.Completed;
            var rnd = new Random();

            foreach(var line in Book.Definition)
            {
                var list = new List<Guid>();
                for(var i=0; i< line; i++)
                {
                    var index = rnd.Next(0, members.Count);
                    var item = members[index];
                    members.RemoveAt(index);
                    list.Add(item);
                }
                Winner.Add(list);
            }
        }

        public void Finish()
        {
            if (State != DataDefinition.SweepstakeThemeState.Started) return;
            State = DataDefinition.SweepstakeThemeState.Finished;
        }

        public void Start()
        {
            if (State != DataDefinition.SweepstakeThemeState.Created) return;
            State = DataDefinition.SweepstakeThemeState.Started;
        }
    }
}
