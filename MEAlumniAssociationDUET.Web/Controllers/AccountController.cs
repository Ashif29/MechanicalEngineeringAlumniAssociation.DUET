using MEAlumniAssociationDUET.Common.Values;
using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Web.Models;
using MEAlumniAssociationDUET.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static MEAlumniAssociationDUET.Common.Values.Permissions;

namespace MEAlumniAssociationDUET.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 

        }

        public IActionResult Login()
        {
            return View();
        } 
    }
    //public class AccountController : Controller
    //{
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly SignInManager<ApplicationUser> _signInManager;
    //    private readonly RoleManager<IdentityRole> _roleManager;

    //    public AccountController(
    //        UserManager<ApplicationUser> userManager, 
    //        SignInManager<ApplicationUser> signInManager, 
    //        RoleManager<IdentityRole> roleManager)
    //    {
    //        _userManager = userManager;
    //        _signInManager = signInManager;
    //        _roleManager = roleManager;
    //    }

    //    [HttpGet]
    //    public IActionResult Register() => View();



    //    public IActionResult Login() => View();

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Login(LoginVM model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
    //            if (result.Succeeded)
    //            {
    //                var user = await _userManager.FindByEmailAsync(model.Email);

    //                if (await _userManager.IsInRoleAsync(user, ConstantsValue.UserRoleName.SuperAdmin))
    //                {
    //                    return RedirectToAction("Index", "AdminDashboard");
    //                }
    //                return RedirectToAction("Index", "Home");
    //            }
    //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    //        }
    //        return View(model);
    //    }

    //    public async Task<IActionResult> Logout()
    //    {
    //        await _signInManager.SignOutAsync();
    //        return RedirectToAction("Index", "Home");
    //    }
    //}
}
