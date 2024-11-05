using MEAlumniAssociationDUET.Common.Enums;
using MEAlumniAssociationDUET.Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Core.Entities
{
    public class ProfessionalInfo:AuditableEntity
    {
        public Guid? PersonalInfoId { get; set; }
        public PersonalInfo? PersonalInfo { get; set; }
        public string? JobTitle { get; set; } 
        public string? CompanyName { get; set; } 
        public string? Department { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public EnumEmployeeType EmploymentType { get; set; } 
       public EnumDesignation Designation { get; set; }

        
    
    }
}
