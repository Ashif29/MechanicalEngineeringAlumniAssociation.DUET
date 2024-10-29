using MEAlumniAssociationDUET.Common.Values;
using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Service.Contracts;
using MEAlumniAssociationDUET.Service.Implementations;
using MEAlumniAssociationDUET.Web.Models;
using MEAlumniAssociationDUET.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static MEAlumniAssociationDUET.Common.Values.Permissions;

namespace MEAlumniAssociationDUET.Web.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly IAuthUserService _authUserService;
       
        public AccountController(IAuthUserService authUserService)
        {
            _authUserService = authUserService;           
        }

        public async Task<IActionResult> Login()
        {
            var LoginModel =  new LoginModel();
            return View(LoginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authUserService.LoginAsync(model.Email, model.Password, model.RememberMe);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authUserService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Register()
        {
            var RegisterModel = new RegisterModel();
            return View(RegisterModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {            
                var result = await model.AddRegisterAsync();
                if (result)
                {
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError(string.Empty, "Registration failed.");
            }
            catch (Exception ex)
            {
                TempData["ErrorNotify"] = ex.Message;
                return View();
            }
            return View(model);
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
