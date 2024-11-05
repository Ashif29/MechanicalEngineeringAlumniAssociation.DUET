using MEAlumniAssociationDUET.Common.Enums;
using MEAlumniAssociationDUET.Core.NotMapped;
using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Service.Contracts;
using MEAlumniAssociationDUET.Service.Result;
using MEAlumniAssociationDUET.Web.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using Autofac;

namespace MEAlumniAssociationDUET.Web.Models
{
    public class ApplicationUserModel:ApplicationUser
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IApplicationRoleService _applicationRoleService;
        private readonly IWebHostEnvironment _host;
        private readonly PhotoSettings _photoSettings;
        private readonly AppSettings _appSettings;
        private readonly IPhotoStorage _photoStorage;
        private readonly SignInManager<ApplicationUser> _signInManager;

        [Required]
        //[Remote("IsExistsUserName", "User", ErrorMessage = "Name Already Exists", AdditionalFields = "InitialUserName")]
        [Display(Name = "User Name")]
        public override string UserName { get; set; }
        [Required]
        //[Remote("IsExistsEmail", "User", ErrorMessage = "Email Already Exists", AdditionalFields = "InitialEmail")]
        public override string Email { get; set; }
        [Required]
        [Display(Name = "User Role")]
        public Guid UserRoleId { get; set; }

        public IFormFile InputFile { get; set; }
        [Display(Name = "User Role Name")]
        public string UserRoleName { get; set; }
        public IList<KeyValuePairObject> UserRoleList { get; set; }
        public string InitialUserName { get; set; }


        public ApplicationUserModel(IApplicationUserService applicationUserService, IApplicationRoleService applicationRoleService, IWebHostEnvironment host, PhotoSettings photoSettings, AppSettings appSettings, IPhotoStorage photoStorage, SignInManager<ApplicationUser> signInManager)
        {
            _applicationUserService = applicationUserService;
            _applicationRoleService = applicationRoleService;
            _host = host;
            _photoSettings = photoSettings;
            _appSettings = appSettings;
            _photoStorage = photoStorage;
            _signInManager = signInManager;
           
        }
        public ApplicationUserModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
            _applicationRoleService = Startup.AutofacContainer.Resolve<IApplicationRoleService>();
            _host = Startup.AutofacContainer.Resolve<IWebHostEnvironment>();
            _signInManager = Startup.AutofacContainer.Resolve<SignInManager<ApplicationUser>>();
            _photoSettings = Startup.AutofacContainer.Resolve<IOptionsSnapshot<PhotoSettings>>().Value;
            _appSettings = Startup.AutofacContainer.Resolve<IOptionsSnapshot<AppSettings>>().Value;
            _photoStorage = Startup.AutofacContainer.Resolve<IPhotoStorage>();
            //var roleResult = _applicationRoleService.GetAllRoleForSelectAsync();
            //UserRoleList = roleResult.Result;
        }
        public IEnumerable<SelectListItem> GetUserRoleSelectList()
        {
            return new SelectList(_applicationRoleService.GetAllUserRoleForSelectAsync().Result, "Id", "Name");
        }

        public ApplicationUserModel(Guid? id) :this()
        {
            if (id.HasValue && id != Guid.Empty)
            {
                var userResult = _applicationUserService.GetByIdAsync(id.Value);
                var userEntry = userResult.Result;
                this.Id = userEntry.Id;
                this.FullName = userEntry.FullName;
                this.UserName = userEntry.UserName;
                this.Email = userEntry.Email;
                this.PhoneNumber = userEntry.PhoneNumber;
                this.Status = userEntry.Status;
                this.ImageUrl = userEntry.ImageUrl;
                if (userEntry.UserRoles.Any())
                {
                    this.UserRoleId = userEntry.UserRoles.FirstOrDefault().RoleId;
                    this.UserRoleName = userEntry.UserRoles.FirstOrDefault().Role.Name;
                }
            }
        }
        public async Task<Result> EditUser()
        {
            var user = new ApplicationUser()
            {
                Id = this.Id,
                FullName = this.FullName,
                UserName = this.UserName,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                Status = ApplicationUserStatus.AlumniUser
            };
            if (this.InputFile != null)
            {
                if (this.InputFile == null)
                    throw new Exception("Null file");
                if (this.InputFile.Length == 0)
                    throw new Exception("Empty file");
                if (this.InputFile.Length > _photoSettings.MaxBytes)
                    throw new Exception("Max file size exceeded");
                if (!_photoSettings.IsSupported(this.InputFile.FileName))
                    throw new Exception("Invalid file type.");
                var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
                var image = await _photoStorage.StorePhoto(uploadsFolderPath, this.InputFile);
                user.ImageUrl = "~/uploads/" + image;
            }
            else
            {
                user.ImageUrl = null;
            }
            var result = await _applicationUserService.UpdateAsync(user, this.UserRoleId);
            return result.Result;
        }

        public async Task ChangePasswordAsync(ChangePasswordModel model)
        {
            await _applicationUserService.ChanagePasswordAsync(model.Id, model.CurrentPassword, model.NewPassword);
            await _signInManager.SignOutAsync();
        }

        public async Task<Result> AddUser()
        {
            var newUser = new ApplicationUser()
            {

                FullName = this.FullName,
                PhoneNumber = this.PhoneNumber,
                UserName = this.UserName,
                Email = this.Email,
                PasswordChangedCount = 0,
                Status = ApplicationUserStatus.AlumniUser
            };
            if (this.InputFile != null)
            {
                if (this.InputFile == null)
                    throw new Exception("Null file");
                if (this.InputFile.Length == 0)
                    throw new Exception("Empty file");
                if (this.InputFile.Length > _photoSettings.MaxBytes)
                    throw new Exception("Max file size exceeded");
                if (!_photoSettings.IsSupported(this.InputFile.FileName))
                    throw new Exception("Invalid file type.");
                var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
                var image = await _photoStorage.StorePhoto(uploadsFolderPath, this.InputFile);
                newUser.ImageUrl = "~/uploads/" + image;
            }
            var result = await _applicationUserService.AddAsync(newUser, this.UserRoleId, _appSettings.UserNewPassword);
            return result.Result;
        }
        public async Task<ApplicationUser> GetByIdAsync(Guid id)
        {
            return await _applicationUserService.GetByIdAsync(id);
        }
        public async Task<QueryResult<ApplicationUser>> GetAllUserAsync(UserQuery model)
        {
            return await _applicationUserService.GetAllAsync(model);
        }
         public async Task<QueryResult<ApplicationUser>> GetAllUserAsync()
        {
            return await _applicationUserService.GetAllAsync();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            return await _applicationUserService.DeleteAsync(id);
        }

        public async Task<Result> ActiveInactiveAsync(Guid id)
        {
            return await _applicationUserService.ActiveInactiveAsync(id);
        }

        public async Task<bool> IsExistsEmailAsync(string email, string initialEmail)
        {
            return await _applicationUserService.IsExistsEmailAsync(email, initialEmail);
        }

        public async Task<bool> IsExistsUserNameAsync(string userName, string initialUserName)
        {
            return await _applicationUserService.IsExistsUserNameAsync(userName, initialUserName);
        }
        public async Task<bool> ResetPassword(Guid id)
        {
            return await _applicationUserService.ResetPassword(id, _appSettings.UserResetPassword);
        }
    }
    public class ChangePasswordModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

