using MEAlumniAssociationDUET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MEAlumniAssociationDUET.Common.Values.Permissions;

namespace MEAlumniAssociationDUET.Repository.Contracts
{
    public interface IApplicationRoleRepository
    {
        Task<bool> CreateRoleAsync(ApplicationRole role);
        Task<bool> RoleExistsAsync(ApplicationRole role);
        Task<bool> RoleExistsAsync(string role);
    }
}
