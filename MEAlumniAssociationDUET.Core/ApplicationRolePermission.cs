using MEAlumniAssociationDUET.Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Core
{
    public class ApplicationRolePermission : AuditableEntity
    {
        public Guid ApplicationPermissionId { get; set; }
        public ApplicationPermission ApplicationPermission { get; set; }
        public Guid ApplicationRoleId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
    }
}
