using MEAlumniAssociationDUET.Common.Enums;
using MEAlumniAssociationDUET.Common.Values;
using MEAlumniAssociationDUET.Core.NotMapped;
using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Service.Contracts;
using MEAlumniAssociationDUET.Service.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MEAlumniAssociationDUET.Repository.Extensions;
using MEAlumniAssociationDUET.Service.Result;

namespace MEAlumniAssociationDUET.Service.Implementations
{
    public class ApplicationRoleService : IApplicationRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationRoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<QueryResult<ApplicationRole>> GetAllAsync(UserRoleQuery queryObj)
        {
            var result = new QueryResult<ApplicationRole>();

            var columnsMap = new Dictionary<string, Expression<Func<ApplicationRole, object>>>()
            {
                ["name"] = v => v.Name,
            };

            var query = _roleManager.Roles.AsQueryable();

            query = query.Where(x => !x.IsDeleted && x.Status != ApplicationRoleStatus.SuperAdmin &&
                (string.IsNullOrWhiteSpace(queryObj.Name) || x.Name.Contains(queryObj.Name)));

            result.TotalItems = await query.CountAsync();
            query = query.ApplyOrdering(queryObj, columnsMap);
            query = query.ApplyPaging(queryObj);
            result.Items = await query.AsNoTracking().ToListAsync();

            return result;
        }

        public async Task<ApplicationRole> GetByIdAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            {
                throw new NotFoundException(nameof(ApplicationRole), id);
            }

            return role;
        }

        public async Task<ApplicationRole> GetByNameAsync(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);

            if (role == null)
            {
                throw new NotFoundException(nameof(ApplicationRole), name);
            }

            return role;
        }

        public async Task<(MEAlumniAssociationDUET.Service.Result.Result Result, Guid Id)> AddAsync(ApplicationRole command)
        {
            var role = new ApplicationRole()
            {
                Name = command.Name,
            };

            var roleSaveResult = await _roleManager.CreateAsync(role);

            if (!roleSaveResult.Succeeded)
            {
                throw new IdentityValidationException(roleSaveResult.Errors);
            }

            role = await _roleManager.FindByNameAsync(command.Name);
            return (roleSaveResult.ToApplicationResult(), role.Id);
        }

        public async Task<(MEAlumniAssociationDUET.Service.Result.Result Result, Guid Id)> UpdateAsync(ApplicationRole command)
        {
            var role = await _roleManager.FindByIdAsync(command.Id.ToString());

            if (role == null)
            {
                throw new NotFoundException(nameof(ApplicationRole), command.Id);
            }

            role.Name = command.Name;
            var roleSaveResult = await _roleManager.UpdateAsync(role);

            if (!roleSaveResult.Succeeded)
            {
                throw new IdentityValidationException(roleSaveResult.Errors);
            }

            return (roleSaveResult.ToApplicationResult(), role.Id);
        }

        public async Task<MEAlumniAssociationDUET.Service.Result.Result> DeleteAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role != null)
            {
                role.IsDeleted = true;
                var result = await _roleManager.UpdateAsync(role);
                return result.ToApplicationResult();
            }

            return MEAlumniAssociationDUET.Service.Result.Result.Success();
        }

        public async Task<MEAlumniAssociationDUET.Service.Result.Result> ActiveInactiveAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role != null)
            {
                role.IsActive = !role.IsActive;
                var result = await _roleManager.UpdateAsync(role);
                return result.ToApplicationResult();
            }

            return MEAlumniAssociationDUET.Service.Result.Result.Success();
        }

        public async Task<IEnumerable<ApplicationRole>> GetAllUserRoleForSelectAsync()
        {
            return await _roleManager.Roles
                .Where(x => x.IsActive && !x.IsDeleted && x.Status != ApplicationRoleStatus.SuperAdmin)
                .ToListAsync();
        }

        public async Task<IList<KeyValuePairObject>> GetAllRoleForSelectAsync()
        {
            return await _roleManager.Roles
                .Where(x => x.IsActive && !x.IsDeleted && x.Status != ApplicationRoleStatus.SuperAdmin)
                .OrderBy(x => x.Name)
                .Select(x => new KeyValuePairObject { Value = x.Id.ToString().ToLower(), Text = x.Name })
                .ToListAsync();
        }
        public async Task<bool> RoleExistsAsync(ApplicationUserRole role)
        {
            return await _roleManager.RoleExistsAsync(role.ToString());
        }
        public async Task<bool> IsExistsNameAsync(string name, string initialName)
        {
            var result = await _roleManager.Roles.AnyAsync(x => x.Name.ToLower() == name.ToLower());
            return result ? (!string.IsNullOrEmpty(initialName) && name.ToLower() == initialName.ToLower() ? false : true) : false;
        }
    }

}
