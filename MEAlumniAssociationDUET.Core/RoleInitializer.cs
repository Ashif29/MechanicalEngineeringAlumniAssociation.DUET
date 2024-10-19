using MEAlumniAssociationDUET.Common.Enums;
using MEAlumniAssociationDUET.Common.Values;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Core
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            // Ensure roles exist
            await EnsureRoleExists(roleManager, ConstantsValue.UserRoleName.SuperAdmin);
            await EnsureRoleExists(roleManager, ConstantsValue.UserRoleName.Admin);
            await EnsureRoleExists(roleManager, ConstantsValue.UserRoleName.AlumniUser);
            await EnsureRoleExists(roleManager, ConstantsValue.UserRoleName.Moderator);

            // Create SuperAdmin user and assign role
            await CreateSuperAdmin(userManager, roleManager);
        }

        // Method to ensure a role exists
        private static async Task EnsureRoleExists(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new ApplicationRole(roleName);
                await roleManager.CreateAsync(role);
            }
        }

        // Method to create a default SuperAdmin user
        private static async Task CreateSuperAdmin(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var superAdminEmail = "superadmin@domain.com";
            var superAdminUser = await userManager.FindByEmailAsync(superAdminEmail);

            if (superAdminUser == null)
            {
                // Create a new SuperAdmin user
                var user = new ApplicationUser
                {
                    UserName = "superadmin",
                    Email = superAdminEmail,
                    FullName = "Super Admin",
                    EmailConfirmed = true, // Optionally set this to false if you want email confirmation flow
                    IsActive = true,
                    Status = ApplicationUserStatus.SuperAdmin // Your custom status enum
                };

                // Create the user with a secure password
                var result = await userManager.CreateAsync(user, "SuperAdminPassword123!@@");  // Set a strong password

                if (result.Succeeded)
                {
                    // Assign the SuperAdmin role
                    await userManager.AddToRoleAsync(user, ConstantsValue.UserRoleName.SuperAdmin);
                }
                else
                {
                    // Handle errors during user creation
                    foreach (var error in result.Errors)
                    {
                        // Log errors or handle them as needed (this is just an example)
                        System.Console.WriteLine(error.Description);
                    }
                }
            }
            else
            {
                // Optionally, ensure SuperAdmin has the correct role if user already exists
                if (!await userManager.IsInRoleAsync(superAdminUser, ConstantsValue.UserRoleName.SuperAdmin))
                {
                    await userManager.AddToRoleAsync(superAdminUser, ConstantsValue.UserRoleName.SuperAdmin);
                }
            }
        }
    }

}
