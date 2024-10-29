using MEAlumniAssociationDUET.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static MEAlumniAssociationDUET.Common.Values.Permissions;

namespace MEAlumniAssociationDUET.Service.Contracts
{
    public interface IAuthUserService
    {
        Task<bool> RegisterAsync(ApplicationUser registerModel);
        Task<bool> LoginAsync(string email, string password, bool rememberMe);
        Task SignOutAsync();
    }
}

