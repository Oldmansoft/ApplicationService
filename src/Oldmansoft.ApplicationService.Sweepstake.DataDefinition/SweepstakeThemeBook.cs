using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.DataDefinition
{
    public class SweepstakeThemeBook
    {
        /// <summary>
        /// 开始
        /// </summary>
        public DateTime Start { get; private set; }

        /// <summary>
        /// 结束
        /// </summary>
        public DateTime Finish { get; private set; }

        /// <summary>
        /// 参与人数下限
        /// </summary>
        public int Floor { get; private set; }

        /// <summary>
        /// 参与人数上限
        /// </summary>
        public int Ceiling { get; private set; }

        /// <summary>
        /// 定义
        /// </summary>
        public List<int> Definition { get; private set; }

        /// <summary>
        /// 当前人数
        /// </summary>
        public int Current { get; private set; }

        private SweepstakeThemeBook() { }

        public static SweepstakeThemeBook Create(DateTime start, DateTime finish, int floor, int ceiling, List<int> definition)
        {
            if (finish < start) throw new ArgumentOutOfRangeException("start");
            if (floor > ceiling) throw new ArgumentOutOfRangeException("floor");
            if (definition == null) throw new ArgumentNullException("definition");
            if (floor < definition.Sum()) throw new ArgumentOutOfRangeException("definition");

            var result = new SweepstakeThemeBook();
            result.Start = start;
            result.Finish = finish;
            result.Floor = floor;
            result.Ceiling = ceiling;
            result.Definition = definition;
            return result;
        }

        public void Change(DateTime start, DateTime finish, int floor, int ceiling, List<int> definition)
        {
            if (finish < start) throw new ArgumentOutOfRangeException("start");
            if (floor > ceiling) throw new ArgumentOutOfRangeException("floor");
            if (definition == null) throw new ArgumentNullException("definition");
            if (floor < definition.Sum()) throw new ArgumentOutOfRangeException("definition");
            
            Start = start;
            Finish = finish;
            Floor = floor;
            Ceiling = ceiling;
            Definition = definition;
        }

        public void SetCurrent(int memberCount)
        {
            Current = memberCount;
        }

        public bool IsFull()
        {
            return Ceiling <= Current;
        }
    }
}
