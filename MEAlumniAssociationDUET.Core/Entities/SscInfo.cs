using MEAlumniAssociationDUET.Core.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MEAlumniAssociationDUET.Common.Values.Permissions;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace MEAlumniAssociationDUET.Core.Entities
{
    public class SscInfo:AuditableEntity
    {
        public Guid? PersonalInfoId { get; set; }
        public PersonalInfo? PersonalInfo { get; set; }
        public string? StudentName { get; set; } 
        public string? RollNumber { get; set; } 
        public string? RegistrationNumber { get; set; } 
        public string? SchoolName { get; set; }
        public string? Board { get; set; } 
        public DateTime? YearOfPassing { get; set; } 
        public string? Grade { get; set; } 
       

    }
}
