using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Application
{
    public abstract class ApplicationBase
    {
        protected IRepositoryFactory Factory { get; private set; }
        
        public ApplicationBase()
        {
            Factory = new Repositories.Factory();
        }
    }
}
