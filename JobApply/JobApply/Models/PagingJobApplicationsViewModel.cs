using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApply.Models
{
    public class PagingJobApplicationsViewModel
    {
        public IEnumerable<JobApplicationListViewModel> JobApplications { get; set; }
        public int TotalPage { get; set; }
    }
}
