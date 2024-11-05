using MEAlumniAssociationDUET.Common.Values;
using MEAlumniAssociationDUET.Core.NotMapped;
using MEAlumniAssociationDUET.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace MEAlumniAssociationDUET.Web.Controllers
{
    using MEAlumniAssociationDUET.Core;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class RoleController : Controller
    {
        private readonly ApplicationRoleModel _applicationRoleModel;

        public RoleController( ApplicationRoleModel applicationRoleModel)
        {
            _applicationRoleModel = applicationRoleModel;
        }

        // GET: Display the list of roles
        public async Task<IActionResult> Index(UserRoleQuery model)
        {
            model.SortBy = "name";
            model.IsSortAscending = true;
            var result = await _applicationRoleModel.GetAllRolesAsync(model);
            var rolesAsPagedList = new StaticPagedList<ApplicationRole>(result.Items, model.Page, model.PageSize, result.TotalItems);
            ViewData["PagedListData"] = rolesAsPagedList;
            return View(model);
        }

        // GET: Add new role form
        [HttpGet]
        public IActionResult Add()
        {
            var model = _applicationRoleModel as ApplicationRoleModel;
            return View(model);
        }

        // POST: Add a new role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ApplicationRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _applicationRoleModel.AddRoleAsync(model);
                if (result)
                {
                    TempData["SuccessNotify"] = "Role has been successfully saved.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorNotify"] = "Role creation failed.";
                }
            }
            return View(model);
        }

        // GET: Edit existing role form
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _applicationRoleModel.GetRoleByIdAsync(id);
            if (model == null)
            {
                TempData["ErrorNotify"] = "Role not found.";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Edit an existing role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _applicationRoleModel.UpdateRoleAsync(model);
                if (result)
                {
                    TempData["SuccessNotify"] = "Role has been successfully updated.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorNotify"] = "Role update failed.";
                }
            }
            return View(model);
        }

        // POST: Delete a role
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _applicationRoleModel.DeleteAsync(id);
            return Json(result);
        }

        // POST: Toggle Active/Inactive status for a role
        [HttpPost]
        public async Task<IActionResult> ActiveInactive(Guid id)
        {
            var result = await _applicationRoleModel.ActiveInactiveAsync(id);
            return Json(result);
        }

        // Check if a role name exists
        public async Task<IActionResult> IsExistsName(string name, string initialName)
        {
            var isExists = await _applicationRoleModel.IsRoleNameExistsAsync(name, initialName);
            return Json(!isExists);
        }
    }

}
