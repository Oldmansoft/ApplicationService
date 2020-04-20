using Oldmansoft.ApplicationService.MoneyBag.Domain;
using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Infrastructure
{
    public interface IWalletRepository : IRepository<Wallet, Guid>
    {
        long All();
    }
}
