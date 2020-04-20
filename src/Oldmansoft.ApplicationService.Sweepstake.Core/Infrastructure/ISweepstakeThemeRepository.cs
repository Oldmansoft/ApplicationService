using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Infrastructure
{
    public interface ISweepstakeThemeRepository : ClassicDomain.IRepository<Domain.SweepstakeTheme, Guid>
    {
        int Count();

        IPagingData<Domain.SweepstakeTheme> Page();

        IList<Guid> ListWaitToStartIds();

        IList<Guid> ListWaitToFinishIds();
    }
}
