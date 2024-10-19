using MEAlumniAssociationDUET.Core.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Core
{
    public class ApplicationPermission : AuditableEntity
    {
        [Display(Name = "Slug")]
        public string Slug { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "UrlPath")]
        public string UrlPath { get; set; }
        [Display(Name = "Http Method")]
        public string HttpMethod { get; set; }

    }
}
