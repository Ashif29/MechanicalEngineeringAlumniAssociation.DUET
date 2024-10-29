using MEAlumniAssociationDUET.Core.NotMapped;
using MEAlumniAssociationDUET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Service.Contracts
{
    public interface IApplicationRoleService
    {
        Task<QueryResult<ApplicationRole>> GetAllAsync(UserRoleQuery queryObj);
        Task<ApplicationRole> GetByIdAsync(Guid id);
        Task<ApplicationRole> GetByNameAsync(string name);
        Task<(MEAlumniAssociationDUET.Service.Result.Result Result, Guid Id)> AddAsync(ApplicationRole command);
        Task<(MEAlumniAssociationDUET.Service.Result.Result Result, Guid Id)> UpdateAsync(ApplicationRole command);
        Task<MEAlumniAssociationDUET.Service.Result.Result> DeleteAsync(Guid id);
        Task<MEAlumniAssociationDUET.Service.Result.Result> ActiveInactiveAsync(Guid id);
        Task<IEnumerable<ApplicationRole>> GetAllUserRoleForSelectAsync();
        Task<IList<KeyValuePairObject>> GetAllRoleForSelectAsync();
        Task<bool> RoleExistsAsync(ApplicationUserRole role);
        Task<bool> IsExistsNameAsync(string name, string initialName);
    }
}
