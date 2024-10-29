using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Core.NotMapped
{
    public class UserQuery : IQueryObject
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid UserRoleId { get; set; }
        public IList<KeyValuePairObject> UserRoles { get; set; }
        public Guid RoleId { get; set; }
        public UserQuery()
        {
            UserRoles = new List<KeyValuePairObject>();
        }
    }
}
