using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Repository.Contracts;
using MEAlumniAssociationDUET.Repository.Core;
using MEAlumniAssociationDUET.Repository.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MEAlumniAssociationDUET.Common.Values.Permissions;

namespace MEAlumniAssociationDUET.Repository.Implementations
{
    public class ApplicationRoleRepository : IApplicationRoleRepository
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationRoleRepository(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRoleAsync(ApplicationRole role)
        {
            if (!await RoleExistsAsync(role))
            {
                var result = await _roleManager.CreateAsync(new ApplicationRole(role.ToString()));
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> RoleExistsAsync(ApplicationRole role)
        {
            return await _roleManager.RoleExistsAsync(role.ToString());
        } 
        public async Task<bool> RoleExistsAsync(string role)
        {
            return await _roleManager.RoleExistsAsync(role);
        }
    }
}
