using MEAlumniAssociationDUET.Common.Enums;
using MEAlumniAssociationDUET.Common.Values;
using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Core.NotMapped;
using MEAlumniAssociationDUET.Service.Contracts;
using MEAlumniAssociationDUET.Service.Result;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using MEAlumniAssociationDUET.Service.Implementations;
using Autofac;

namespace MEAlumniAssociationDUET.Web.Models
{
    public class ApplicationRoleModel:ApplicationRole
    {
        private readonly IApplicationRoleService _applicationRoleService;
        private readonly RoleManager<ApplicationRole> _roleManager;


        public ApplicationRoleModel(IApplicationRoleService applicationRoleService, RoleManager<ApplicationRole> roleManager)
        {
            _applicationRoleService = applicationRoleService;
            _roleManager = roleManager;
        }
        public ApplicationRoleModel()
        {
            _applicationRoleService = Startup.AutofacContainer.Resolve<IApplicationRoleService>();        
            _roleManager = Startup.AutofacContainer.Resolve<RoleManager<ApplicationRole>>();           
        }
        public ApplicationRoleModel(Guid id) : this()
        {

            //var role = _roleManager.FindByIdAsync(id.ToString());
            var role = _applicationRoleService.GetByIdAsync(id);
            
        }
            public async Task<QueryResult<ApplicationRole>> GetAllRolesAsync(UserRoleQuery query)
        {
            return await _applicationRoleService.GetAllAsync(query);
        }

        public async Task<bool> AddRoleAsync(ApplicationRoleModel roleModel)
        {
            var role = new ApplicationRole
            {
                Name = roleModel.Name,
                Status = ApplicationRoleStatus.AlumniUser // Or whatever default status you need
            };

            var identityResult = await _roleManager.CreateAsync(role);
            return identityResult.Succeeded;
        }

        public async Task<ApplicationRole> GetRoleByIdAsync(Guid id)
        {
            var role = await _applicationRoleService.GetByIdAsync(id);

            if (role != null)
            {
                return new ApplicationRole
                {
                    Id = role.Id,
                    Name = role.Name,
                    Status = role.Status,
                };
            }

            return null;
        }


        public async Task<bool> UpdateRoleAsync(ApplicationRoleModel roleModel)
        {
            var existingRole = await _roleManager.FindByIdAsync(roleModel.Id.ToString());
            if (existingRole != null)
            {
                existingRole.Name = roleModel.Name;
                existingRole.Status = roleModel.Status;
                var result = await _roleManager.UpdateAsync(existingRole);
                return result.Succeeded;
            }
            return false;
        }
        public async Task<IList<KeyValuePairObject>> GetAllRoleForSelectAsync()
        {
            return await _applicationRoleService.GetAllRoleForSelectAsync();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            return await _applicationRoleService.DeleteAsync(id);
        }

        public async Task<Result> ActiveInactiveAsync(Guid id)
        {
            return await _applicationRoleService.ActiveInactiveAsync(id);
        }

        public async Task<bool> IsRoleNameExistsAsync(string name, string initialName)
        {
            return await _applicationRoleService.IsExistsNameAsync(name, initialName);
        }
    }


}
