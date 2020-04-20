using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Infrastructure
{
    public interface ISweepstakeThemeMemberRepository : ClassicDomain.IRepository<Domain.SweepstakeThemeMember, Guid>
    {
        int Count(Guid themeId);

        bool Has(Guid themeId, Guid memberId);

        IList<Guid> ListMemberId(Guid themeId);

        IList<Domain.SweepstakeThemeMember> List(Guid themeId, int skip, int count);
    }
}
