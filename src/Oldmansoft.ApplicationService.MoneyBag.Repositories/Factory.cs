using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.ApplicationService.MoneyBag.Infrastructure;
using Oldmansoft.ClassicDomain;

namespace Oldmansoft.ApplicationService.MoneyBag.Repositories
{
    public class Factory : IRepositoryFactory
    {
        private UnitOfWork Uow { get; set; }

        public Factory()
        {
            Uow = new UnitOfWork();
        }

        public IUnitOfWork GetUnitOfWork()
        {
            return Uow;
        }

        public IAppClientRepository CreateAppClientRepository()
        {
            return new AppClientRepository(Uow);
        }

        public IBillingRepository CreateBillingRepository()
        {
            return new BillingRepository(Uow);
        }

        public IInnerQueueRepository CreateInnerQueueRepository()
        {
            return new InnerQueueRepository(Uow);
        }

        public IWalletRepository CreateWalletRepository()
        {
            return new WalletRepository(Uow);
        }
    }
}
