using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApply.Models
{
    public class JobOffer
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string JobDescription { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public DateTime WorkStartDate { get; set; }
        public string Location { get; set; }
        public int SalaryFrom{ get; set; }
        public int SalaryTo { get; set; }
        public string SalaryDescription { get; set; }
        public string ContractLength { get; set; }
        public DateTime Created { get; set; }
        public List<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    }
}
