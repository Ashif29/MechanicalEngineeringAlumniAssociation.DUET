using MEAlumniAssociationDUET.Common.Enums;
using MEAlumniAssociationDUET.Core.NotMapped;
using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Service.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MEAlumniAssociationDUET.Repository.Extensions;
using MEAlumniAssociationDUET.Service.Exceptions;
using MEAlumniAssociationDUET.Service.Result;

namespace MEAlumniAssociationDUET.Service.Implementations
{
    public class ApplicationUserService:IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationUserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager
            )
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager;
        }



        public async Task<QueryResult<ApplicationUser>> GetAllAsync(UserQuery queryObj)
        {
            var result = new QueryResult<ApplicationUser>
            {
                Items = new List<ApplicationUser>()
            };

            var columnsMap = new Dictionary<string, Expression<Func<ApplicationUser, object>>>()
            {
                ["fullName"] = v => v.FullName,
                ["userName"] = v => v.UserName,
                ["email"] = v => v.Email,
            };

            var query = _userManager.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .AsQueryable();

            query = query.Where(x => !x.IsDeleted && x.Status != ApplicationUserStatus.SuperAdmin &&
                (string.IsNullOrWhiteSpace(queryObj.FullName) || x.FullName.Contains(queryObj.FullName)) &&
                (string.IsNullOrWhiteSpace(queryObj.UserName) || x.UserName.Contains(queryObj.UserName)) &&
                (queryObj.UserRoleId == null || x.UserRoles.FirstOrDefault().RoleId.Equals(queryObj.UserRoleId)) &&
                (string.IsNullOrWhiteSpace(queryObj.Email) || x.Email.Contains(queryObj.Email)));

            result.TotalItems = await query.CountAsync();
            query = query.ApplyOrdering(queryObj, columnsMap);
            query = query.ApplyPaging(queryObj);
            result.Items = await query.AsNoTracking().ToListAsync();

            return result;
        }

        public virtual async Task<QueryResult<ApplicationUser>> GetAllAsync()
        {
            var result = new QueryResult<ApplicationUser>();

            var columnsMap = new Dictionary<string, Expression<Func<ApplicationUser, object>>>()
            {
                ["created"] = v => v.Created
                //["UserRoleId"] = v => v.UserRoles.FirstOrDefault().RoleId
            };

            var query = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();

            query = query.Where(x => !x.IsDeleted && x.Status != ApplicationUserStatus.SuperAdmin);

            result.TotalItems = await query.CountAsync();
            //query = query.ApplyOrdering(queryObj, columnsMap);
            result.Items = (await query.AsNoTracking().ToListAsync());

            return result;
        }

        public async Task<ApplicationUser> GetByIdAsync(Guid id)
        {
            var query = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();

            var user = await query.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), id);
            }

            return user;
        }

        public async Task<ApplicationUser> GetByUserNameAsync(string userName)
        {
            var query = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();

            var user = await query.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), userName);
            }

            return user;
        }

        public async Task<(MEAlumniAssociationDUET.Service.Result.Result Result, Guid Id)> AddAsync(ApplicationUser command, Guid userRoleId, string newPassword)
        {
            var user = new ApplicationUser
            {
                UserName = command.UserName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                FullName = command.FullName,
                ImageUrl = command.ImageUrl ?? string.Empty,
                Status = command.Status
            };

            var userSaveResult = await _userManager.CreateAsync(user, newPassword);

            if (!userSaveResult.Succeeded)
            {
                throw new IdentityValidationException(userSaveResult.Errors);
            };

            // Add New User Role
            user = await _userManager.FindByNameAsync(user.UserName);
            var role = await _roleManager.FindByIdAsync(userRoleId.ToString());

            if (role == null)
            {
                throw new NotFoundException(nameof(ApplicationRole), userRoleId);
            }

            var roleSaveResult = await _userManager.AddToRoleAsync(user, role.Name);

            if (!roleSaveResult.Succeeded)
            {
                throw new IdentityValidationException(roleSaveResult.Errors);
            };

            return (userSaveResult.ToApplicationResult(), user.Id);
        }

        public async Task<(MEAlumniAssociationDUET.Service.Result.Result Result, Guid Id)> UpdateAsync(ApplicationUser command, Guid userRoleId)
        {
            var user = await this._userManager.FindByIdAsync(command.Id.ToString());

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), command.Id);
            }

            user.UserName = command.UserName;
            user.Email = command.Email;
            user.PhoneNumber = command.PhoneNumber;
            user.FullName = command.FullName;
            ///user.ImageUrl = command.ImageUrl ?? user.ImageUrl;
            user.ImageUrl = command.ImageUrl != null ? command.ImageUrl : user.ImageUrl;
            var userSaveResult = await _userManager.UpdateAsync(user);

            if (!userSaveResult.Succeeded)
            {
                throw new IdentityValidationException(userSaveResult.Errors);
            };

            // Remove Previous User Role
            var previousUserRoles = await _userManager.GetRolesAsync(user);
            if (previousUserRoles.Any())
            {
                var roleRemoveResult = await _userManager.RemoveFromRolesAsync(user, previousUserRoles);

                if (!roleRemoveResult.Succeeded)
                {
                    throw new IdentityValidationException(roleRemoveResult.Errors);
                };

            }

            // Add New User Role
            var role = await _roleManager.FindByIdAsync(userRoleId.ToString());

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationRole), userRoleId);
            }

            var roleSaveResult = await _userManager.AddToRoleAsync(user, role.Name);

            if (!roleSaveResult.Succeeded)
            {
                throw new IdentityValidationException(roleSaveResult.Errors);
            };


            return (userSaveResult.ToApplicationResult(), user.Id);
        }

        public async Task<MEAlumniAssociationDUET.Service.Result.Result> DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                user.IsDeleted = true;
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    throw new IdentityValidationException(result.Errors);
                };

                return result.ToApplicationResult();
            }

            return MEAlumniAssociationDUET.Service.Result.Result.Success();
        }

        public async Task<MEAlumniAssociationDUET.Service.Result.Result> ActiveInactiveAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                user.IsActive = !user.IsActive;
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    throw new IdentityValidationException(result.Errors);
                };

                return result.ToApplicationResult();
            }

            return MEAlumniAssociationDUET.Service.Result.Result.Success();
        }

        public async Task<IList<KeyValuePairObject>> GetAllForSelectAsync()
        {
            return await _userManager.Users.Where(x => x.IsActive && !x.IsDeleted).OrderBy(x => x.FullName)
                                .Select(s => new KeyValuePairObject { Value = s.Id.ToString().ToLower(), Text = s.FullName }).ToListAsync();
        }


        public async Task<bool> IsExistsUserNameAsync(string name, string initialName)
        {
            var result = await _userManager.Users.AnyAsync(x => !x.IsDeleted && x.UserName.ToLower() == name.ToLower());
            result = result ? (!string.IsNullOrEmpty(initialName) && name.ToLower() == initialName.ToLower() ? false : true) : false;
            return result;
        }

        public async Task<bool> IsExistsEmailAsync(string email, string initialEmail)
        {
            var result = await _userManager.Users.AnyAsync(x => !x.IsDeleted && x.Email.ToLower() == email.ToLower());
            result = result ? (!string.IsNullOrEmpty(initialEmail) && email.ToLower() == initialEmail.ToLower() ? false : true) : false;
            return result;
        }
        public async Task<long> GetUsersCountAsync()
        {
            return await _userManager.Users.CountAsync(x => x.IsActive && !x.IsDeleted);
        }

        public async Task ChanagePasswordAsync(Guid id, string currentPassword, string newPassword)
        {

            var user = await GetByIdAsync(id);
            var checkPassword = await _userManager.CheckPasswordAsync(user, currentPassword);
            if (!checkPassword)
            {
                throw new Exception("Current Password Not Valid");
            }
            await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        }
        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }


        public async Task<bool> ResetPassword(Guid id, string encryptPassword)
        {

            var user = await this._userManager.FindByIdAsync(id.ToString());

            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, encryptPassword);

            if (result.Succeeded) return true;
            else return false;


        }

    }
}
