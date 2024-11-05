using MEAlumniAssociationDUET.Service.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Service.Implementations
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            Guid.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out Guid value);
            UserId = value;
            IsAuthenticated = httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
            Name = httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "";
        }
        public Guid UserId { get; }
        public string Name { get; }
        public bool IsAuthenticated { get; }
    }
}
