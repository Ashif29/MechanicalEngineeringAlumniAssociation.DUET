using MEAlumniAssociationDUET.Common.Enums;
using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Service.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static MEAlumniAssociationDUET.Common.Values.Permissions;
using MEAlumniAssociationDUET.Repository.Contracts;

namespace MEAlumniAssociationDUET.Service.Implementations
{
    public class AuthUserService : IAuthUserService
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IApplicationRoleRepository _userRoleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthUserService(IApplicationUserRepository userRepository,IApplicationRoleRepository applicationUserRoleRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userRoleRepository = applicationUserRoleRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> RegisterAsync(ApplicationUser user)
        {         
            var userCreated = await _userRepository.CreateUserAsync(user, user.LastPassword);
            if (userCreated)
            {
                    var role = ApplicationUserStatus.UnAuthenticatedUser.ToString();
                    var isRoleExist = await _userRoleRepository.RoleExistsAsync(role);
                    if (!isRoleExist)
                    {
                       var newRole = new ApplicationRole(role);
                       await _userRoleRepository.CreateRoleAsync(newRole);
                    
                    }
                    var roles = new List<string> { role };                             
                    await _userRepository.AssignRoleAsync(user, roles);              
                    return true;
            }
            return false;
        }

        public async Task<bool> LoginAsync(string email, string password, bool rememberMe)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            if (user != null && await _userRepository.CheckPasswordAsync(user, password))
            {
                var userRoles = await _userRepository.GetRolesAsync(user); // Assume this exists in repository
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Status.ToString())
                };

                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                await _userRepository.AssignClaimsAsync(user, claims);
                var identity = new ClaimsIdentity(claims, "AuthCookie");
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = rememberMe
                };

                await _httpContextAccessor.HttpContext.SignInAsync("AuthCookie", principal, authProperties);
                return true;
            }
            return false;
        }
        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync("AuthCookie");
        }
    }
}
