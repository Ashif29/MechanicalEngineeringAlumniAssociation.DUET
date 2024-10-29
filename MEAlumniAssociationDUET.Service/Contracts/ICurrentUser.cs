using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Service.Contracts
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string Name { get; }
        bool IsAuthenticated { get; }
    }
}
