using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Application
{
    public class ThemeMember : ApplicationBase
    {
        public int Count(Guid id)
        {
            return Factory.CreateSweepstakeThemeMemberRepository().Count(id);
        }

        public IList<Data.ThemeMemberData> List(Guid id, int skip, int count)
        {
            var list = Factory.CreateSweepstakeThemeMemberRepository().List(id, skip, count);
            return list.MapTo(new List<Data.ThemeMemberData>());
        }
    }
}
