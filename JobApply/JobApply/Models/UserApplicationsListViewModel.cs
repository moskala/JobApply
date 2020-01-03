using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApply.Models
{
    public class UserApplicationsListViewModel
    {
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public int ApplicationId { get; set; }
        public int OfferId { get; set; }
    }
}
