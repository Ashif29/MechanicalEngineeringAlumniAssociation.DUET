using MEAlumniAssociationDUET.Common.Enums;
using MEAlumniAssociationDUET.Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Core.Entities
{
    public class DuetInfo:AuditableEntity
    {
        public Guid? PersonalInfoId { get; set; }
        public PersonalInfo? PersonalInfo { get; set; }
        public string? StudentId {  get; set; }
        public string? DepartmentName { get; set; } = "ME";
        public DateTime? PassingYear { get; set; }
        public EnumBatch Batch {  get; set; }
        public EnumHostel HostelName {  get; set; }
        public string? RoomNumber {  get; set; }


    }
}
