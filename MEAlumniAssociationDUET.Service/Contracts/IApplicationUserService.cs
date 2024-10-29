using MEAlumniAssociationDUET.Core.NotMapped;
using MEAlumniAssociationDUET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Service.Contracts
{
    public interface IApplicationUserService
    {
        Task<QueryResult<ApplicationUser>> GetAllAsync(UserQuery queryObj);
        Task<ApplicationUser> GetByIdAsync(Guid id);
        Task<ApplicationUser> GetByUserNameAsync(string userName);
        Task<(MEAlumniAssociationDUET.Service.Result.Result Result, Guid Id)> AddAsync(ApplicationUser command, Guid userRoleId, string newPassword);
        Task<(MEAlumniAssociationDUET.Service.Result.Result Result, Guid Id)> UpdateAsync(ApplicationUser command, Guid userRoleId);
        Task<MEAlumniAssociationDUET.Service.Result.Result> DeleteAsync(Guid id);
        Task<MEAlumniAssociationDUET.Service.Result.Result> ActiveInactiveAsync(Guid id);
        Task<IList<KeyValuePairObject>> GetAllForSelectAsync();
        Task<bool> IsExistsUserNameAsync(string name, string initialName);
        Task<bool> IsExistsEmailAsync(string email, string initialEmail);
        Task<long> GetUsersCountAsync();
        Task ChanagePasswordAsync(Guid id, string currentPassword, string newPassword);
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<bool> ResetPassword(Guid id, string encryptPassword);
        Task<QueryResult<ApplicationUser>> GetAllAsync();
    }
}
