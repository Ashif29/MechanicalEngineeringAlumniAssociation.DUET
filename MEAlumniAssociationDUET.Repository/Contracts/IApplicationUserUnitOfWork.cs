using MEAlumniAssociationDUET.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Repository.Contracts
{
    public interface IApplicationUserUnitOfWork:IUnitOfWork
    {
        public IApplicationUserRepository ApplicationUserRepositoryq { get; }
    }
}
