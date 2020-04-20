using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Repositories
{
    class SweepstakeThemeRepository : ClassicDomain.Driver.Mongo.Repository<Domain.SweepstakeTheme, Guid, Mapping>, Infrastructure.ISweepstakeThemeRepository
    {
        public SweepstakeThemeRepository(UnitOfWork uow)
            : base(uow)
        { }

        public IPagingData<Domain.SweepstakeTheme> Page()
        {
            return Query().Paging().OrderByDescending(o => o.Id);
        }

        public int Count()
        {
            return Query().Count();
        }

        public IList<Guid> ListWaitToStartIds()
        {
            return Query().Where(o => o.State == DataDefinition.SweepstakeThemeState.Created && o.Book.Start < DateTime.UtcNow).Select(o => o.Id).ToList();
        }

        public IList<Guid> ListWaitToFinishIds()
        {
            return Query().Where(o => o.State == DataDefinition.SweepstakeThemeState.Started && o.Book.Finish < DateTime.UtcNow).Select(o => o.Id).ToList();
        }
    }
}
