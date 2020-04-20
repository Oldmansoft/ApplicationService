using Oldmansoft.ApplicationService.MoneyBag.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Repositories
{
    class RedisMapping : ClassicDomain.Driver.Redis.FastModeContext
    {
        protected override void OnModelCreating()
        {
            Add<InnerQueue, Guid>(o => o.Id);
        }
    }
}
