using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static MEAlumniAssociationDUET.Common.Values.Permissions;

namespace MEAlumniAssociationDUET.Repository.Contracts
{
    public interface IApplicationUserRepository
    {
        Task<bool> CreateUserAsync(ApplicationUser user, string password);
        Task<bool> AssignRoleAsync(ApplicationUser user, IEnumerable<string> roles);
        Task<bool> AssignClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    }
}
