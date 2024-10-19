using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Common.Values
{
    public static class ConstantsValue
    {
        public static UserRoleName UserRoleName => new UserRoleName();
        public static RolePermission RolePermission => new RolePermission();
    }

    public class UserRoleName
    {
        public string SuperAdmin => "SuperAdmin";
        public string Admin => "Admin";
        public string AlumniUser => "Alumni User";
        public string Moderator => "Moderator";
    }

    public class RolePermission
    {
        public string Type => "Permission";
        public string Value => "Permissions.SuperAdmin";
    }
}
