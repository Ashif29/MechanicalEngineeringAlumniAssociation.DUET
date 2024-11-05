using MEAlumniAssociationDUET.Core.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEAlumniAssociationDUET.Common.Enums;

namespace MEAlumniAssociationDUET.Core.Entities
{
    public class PersonalInfo:AuditableEntity
    {
        [DisplayName("Alomni ID")]
        public string? AlomniId { get; set; }

        [DisplayName("First Name")]
        public string? FirstName { get; set; }

        [DisplayName("Middle Name")]
        public string? MiddleName { get; set; }

        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        [DisplayName("Gender")]
        public EnumGender? Gender { get; set; }

        [DisplayName("DOB (Date-of-Birth)")]
        public DateTime? DOB { get; set; }

        [DisplayName("Age")]
        public float? Age { get; set; }

        [DisplayName("Mobile")]
        public string? Mobile { get; set; }

        [DisplayName("E-mail")]
        public string? Email { get; set; }
        [DisplayName("Emergency Contact Person")]
        public string? EmergencyConPerson { get; set; }

        [DisplayName("Emergency Contact Relationship")]
        public string? EmergencyConRelationship { get; set; }

        [DisplayName("Phone No")]
        public string? EmergencyConPhoneNo { get; set; }
        [DisplayName("Photo")]
        public string? ImageSrc { get; set; }
        [DisplayName("Registration Date")]
        public DateTime? RegistrationDate { get; set; }
        [DisplayName("Language")]
        public string? Language { get; set; }
        public string? City { get; set; }
        public string? Division { get; set; }
        public EnumBlood BloodGroup {  get; set; }
        public IList<DuetInfo>? DuetInfos { get; set;}
        public IList<DiplomaInfo>? DiplomaInfos { get; set; }
        public IList<SscInfo>? SscInfos { get; set;}
        public IList<ProfessionalInfo>? ProfessionalInfos { get; set; }
        public IList<HigherStudyInfo>? HigherStudyInfos { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }
    }
}
