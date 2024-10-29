using MEAlumniAssociationDUET.Common.Enums;
using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Repository.Contracts;
using MEAlumniAssociationDUET.Repository.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static MEAlumniAssociationDUET.Common.Values.Permissions;

namespace MEAlumniAssociationDUET.Repository.Implementations
{
    public class ApplicationUserRepository:IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> AssignRoleAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }
        public async Task<bool> AssignClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims)
        {
            var result = await _userManager.AddClaimsAsync(user, claims);
            return result.Succeeded; // Return whether claims were successfully assigned
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
