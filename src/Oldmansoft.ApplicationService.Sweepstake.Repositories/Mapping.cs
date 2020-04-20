using Oldmansoft.ApplicationService.Sweepstake.Domain;
using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Repositories
{
    class Mapping : ClassicDomain.Driver.Mongo.Context
    {
        protected override void OnModelCreating()
        {
            Add<SweepstakeMission, string>(o => o.Id);

            Add<SweepstakeTheme, Guid>(o => o.Id)
                .SetIndex(g => g.CreateGroup(o => o.State).Add(o => o.Book.Finish));

            Add<SweepstakeThemeMember, Guid>(o => o.Id)
                .SetUnique(g => g.CreateGroup(o => o.ThemeId).Add(o => o.MemberId));
        }
    }
}
