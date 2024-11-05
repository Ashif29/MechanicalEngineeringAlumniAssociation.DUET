using MEAlumniAssociationDUET.Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Core.Entities
{
    public class DiplomaInfo:AuditableEntity
    {
        public Guid? PersonalInfoId { get; set; }
        public PersonalInfo? PersonalInfo { get; set; }
        public string? Polytechnique { get; set; }
        public string? DepartmentName {  get; set; }
        public DateTime? PassingYear {  get; set; }
        public string? Session { get; set; }
        public string? CGPA {  get; set; }

    }
}
