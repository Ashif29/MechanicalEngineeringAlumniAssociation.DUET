using MEAlumniAssociationDUET.Core.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Core.Entities
{
    public class AlumniUser : AuditableEntity
    {

        public string Department { get; set; }
        public int GraduationYear { get; set; }

        // Foreign key to ApplicationUser
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public AlumniUser()
        {
            // Add any necessary default values here
        }
    }

}
