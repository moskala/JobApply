using JobApply.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApply.Models
{
    public class JobApplicationViewModel
    {
        public int ApplicationId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Required]
        [Display(Name = "Contact Aggrement")]
        public bool ContactAgreement { get; set; }
        [Display(Name = "CV File")]
        public string CvUrl { get; set; }
        public int OfferId { get; set; }
        [Display(Name = "Sent")]
        public DateTime Created { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Location")]
        public string Location { get; set; }


        public static implicit operator JobApplication(JobApplicationViewModel vm)
        {
            return new JobApplication
            {
                Id = vm.ApplicationId,
                OfferId = vm.OfferId,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                PhoneNumber = vm.PhoneNumber,
                EmailAddress = vm.EmailAddress,
                ContactAgreement = vm.ContactAgreement,
                CvUrl = vm.CvUrl,
                Created = vm.Created,
            };
        }

        public static implicit operator JobApplicationViewModel(JobApplication vm)
        {
            return new JobApplicationViewModel
            {
                ApplicationId = vm.Id,
                OfferId = vm.OfferId,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                PhoneNumber = vm.PhoneNumber,
                EmailAddress = vm.EmailAddress,
                ContactAgreement = vm.ContactAgreement,
                CvUrl = vm.CvUrl,
                Created = vm.Created,
            };
        }
    }
}
