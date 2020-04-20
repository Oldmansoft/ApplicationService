using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Repositories
{
    class SweepstakeThemeMemberRepository : ClassicDomain.Driver.Mongo.Repository<Domain.SweepstakeThemeMember, Guid, Mapping>, Infrastructure.ISweepstakeThemeMemberRepository
    {
        public SweepstakeThemeMemberRepository(UnitOfWork uow)
            : base(uow)
        { }

        public int Count(Guid themeId)
        {
            return Query().Where(o => o.ThemeId == themeId).Count();
        }

        public bool Has(Guid themeId, Guid memberId)
        {
            return Query().Where(o => o.ThemeId == themeId && o.MemberId == memberId).FirstOrDefault() != null;
        }

        public IList<Guid> ListMemberId(Guid themeId)
        {
            return Query().Where(o => o.ThemeId == themeId).Select(o => o.MemberId).ToList();
        }

        public IList<Domain.SweepstakeThemeMember> List(Guid themeId, int skip, int count)
        {
            return Query().Where(o => o.ThemeId == themeId).Skip(skip).Take(count).ToList();
        }
    }
}
