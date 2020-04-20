using Oldmansoft.ApplicationService.MoneyBag.Infrastructure;
using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag
{
    public interface IRepositoryFactory
    {
        IUnitOfWork GetUnitOfWork();

        IWalletRepository CreateWalletRepository();

        IBillingRepository CreateBillingRepository();

        IAppClientRepository CreateAppClientRepository();

        IInnerQueueRepository CreateInnerQueueRepository();
    }
}
