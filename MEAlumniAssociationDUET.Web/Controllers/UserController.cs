using MEAlumniAssociationDUET.Core.NotMapped;
using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Web.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace MEAlumniAssociationDUET.Web.Controllers
{
    public class UserController : Controller
    {      
        public async Task<IActionResult> Index(UserQuery model)                        
        {
            model.PageSize = 50;
            model.UserRoles = await new ApplicationRoleModel().GetAllRoleForSelectAsync();
            var result = await new ApplicationUserModel().GetAllUserAsync(model);
            var usersAsIPagedList = new StaticPagedList<ApplicationUser>(result.Items, model.Page, model.PageSize, result.TotalItems);
            ViewData["PagedListData"] = usersAsIPagedList;
            // ViewBag.CurrentUserId = _currentUserService.UserId;
            return View(model);
        }
        public async Task<IActionResult> Profile(Guid id)
        {
            var model = await Task.Run(() => new ApplicationUserModel(id));
            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var model = await Task.Run(() => new ApplicationUserModel(id));
            return View("_Details", model);
        }
        [HttpGet]

        public async Task<IActionResult> Add()
        {
            var model = await Task.Run(() => new ApplicationUserModel());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ApplicationUserModel model)
        {
            var isUserNameExists = await model.IsExistsUserNameAsync(model.UserName, model.InitialUserName);
            if (ModelState.IsValid && !isUserNameExists)
            {
                var result = await model.AddUser();
                if (result.Succeeded)
                {
                    TempData["SuccessNotify"] = "User Added Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await Task.Run(() => new ApplicationUserModel(id));
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUserModel model)
        {
            var isUserNameExists = await model.IsExistsUserNameAsync(model.UserName, model.InitialUserName);
            if (ModelState.IsValid && !isUserNameExists)
            {
                var result = await model.EditUser();

                if (result.Succeeded)
                {
                    TempData["SuccessNotify"] = "User Updated Successfully";
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await new ApplicationUserModel().DeleteAsync(id);
            return Json(result);
        }

        public async Task<IActionResult> ActiveInactive(Guid id)
        {
            var result = await new ApplicationUserModel().ActiveInactiveAsync(id);
            return Json(result);
        }
        public async Task<IActionResult> ResetPassword(Guid id)
        {
            var result = await new ApplicationUserModel().ResetPassword(id);
            return Json(result);
        }
        public async Task<IActionResult> IsExistsUserName(string UserName, string InitialUserName)
        {
            var isExists = await new ApplicationUserModel().IsExistsUserNameAsync(UserName, InitialUserName);
            return Json(!isExists);
        }
        public async Task<IActionResult> IsExistsEmail(string Email, string InitialEmail)
        {
            var isExists = await new ApplicationUserModel().IsExistsEmailAsync(Email, InitialEmail);
            return Json(!isExists);
        }
    }
}
