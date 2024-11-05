using MEAlumniAssociationDUET.Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Core.Entities
{
    public class HigherStudyInfo : AuditableEntity
    {
        public Guid? PersonalInfoId { get; set; }
        public PersonalInfo? PersonalInfo { get; set; }
        public string? DegreeName { get; set; }         
        public string? HierarchyName { get; set; }    
        public string? UniversityName { get; set; }     
        public string? ProgramType { get; set; }         
        public int DurationInYears { get; set; }        
        public string? Specialization { get; set; }     
        public string? Description { get; set; }        
        public string? Location { get; set; }            
        public decimal TuitionFee { get; set; }           
        public DateTime StartDate { get; set; }        
        public DateTime EndDate { get; set; }            
      
    }

}
