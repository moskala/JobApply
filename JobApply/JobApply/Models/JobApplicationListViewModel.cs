using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApply.Models
{
    public class JobApplicationListViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public static implicit operator JobApplicationListViewModel(JobApplication vm)
        {
            return new JobApplicationListViewModel
            {
                Id = vm.Id,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                EmailAddress = vm.EmailAddress,
            };
        }
    }
}
