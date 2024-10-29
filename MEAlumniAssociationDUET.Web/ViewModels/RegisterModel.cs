using static MEAlumniAssociationDUET.Common.Values.Permissions;
using System.ComponentModel.DataAnnotations;
using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Service.Contracts;
using MEAlumniAssociationDUET.Web.Core;
using Microsoft.AspNetCore.Identity;
using Autofac;
using Microsoft.Extensions.Options;
using MEAlumniAssociationDUET.Common.Enums;

namespace MEAlumniAssociationDUET.Web.ViewModels
{
    public class RegisterModel:ApplicationUser
    {
        private readonly IAuthUserService _authUserService;
        private readonly IWebHostEnvironment _host;        
        private readonly IPhotoStorage _photoStorage;  
        private readonly ICurrentUserService _currentUserService;
        public RegisterModel( IAuthUserService authUserService, IWebHostEnvironment host, IPhotoStorage photoStorage, ICurrentUserService currentUserService)
        {
            _authUserService = authUserService;       
            _host = host;      
            _photoStorage = photoStorage;
            _currentUserService = currentUserService;
        }


        public IFormFile AlumniImage { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public RegisterModel()
        {
            _authUserService = Startup.AutofacContainer.Resolve<IAuthUserService>();
            _host = Startup.AutofacContainer.Resolve<IWebHostEnvironment>();
            _photoStorage = Startup.AutofacContainer.Resolve<IPhotoStorage>();
            _currentUserService=Startup.AutofacContainer.Resolve<ICurrentUserService>();            
        }

        public async Task<bool> AddRegisterAsync()
        {
            UserName = Email;    
            LastPassChangeDate = DateTime.Now;
            LastPassword = Password;
            Status = ApplicationUserStatus.UnAuthenticatedUser;
            CreatedBy = _currentUserService.UserId;
            Created = DateTime.Now;
            PasswordChangedCount = 0;
            // save registration  
            ImageUrl = AlumniImage != null ? await SaveImageAsync(AlumniImage) : null;
            var isReg = await _authUserService.RegisterAsync(this);
            return isReg;
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads\\alumni_file\\alumniImages");

            var imageName = await _photoStorage.StorePhoto(uploadsFolderPath, file);
            return @"\uploads\alumni_file\alumniImages\" + imageName;
        }

    }
}
