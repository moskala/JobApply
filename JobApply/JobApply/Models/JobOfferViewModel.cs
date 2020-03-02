using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApply.Models
{
    public class JobOfferViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }

        [Required]
        [CheckDate]
        [Display(Name = "Application Deadline")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime ApplicationDeadline { get; set; }

        [Required]
        [Display(Name = "Work start date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime WorkStartDate { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Salary must be greater than zero")]
        [Display(Name = "Salary from")]
        public int SalaryFrom { get; set; }

        [Required]
        [SalaryGreaterThan("SalaryFrom")]
        [Display(Name = "Salary to")]
        public int SalaryTo { get; set; }

        [Required]
        [Display(Name = "Salary Description")]
        public string SalaryDescription { get; set; }

        [Required]
        [Display(Name = "Contract Length")]
        public string ContractLength { get; set; }
        public List<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

        public static implicit operator JobOffer(JobOfferViewModel vm)
        {
            return new JobOffer
            {
                Id = vm.Id,
                JobTitle = vm.JobTitle,
                CompanyName = vm.CompanyName,
                JobDescription = vm.JobDescription,
                ApplicationDeadline = vm.ApplicationDeadline,
                WorkStartDate = vm.WorkStartDate,
                Location = vm.Location,
                SalaryFrom = vm.SalaryFrom,
                SalaryTo = vm.SalaryTo,
                SalaryDescription = vm.SalaryDescription,
                ContractLength = vm.ContractLength,
                Created = DateTime.Now,
            };   
        }

        public static implicit operator JobOfferViewModel(JobOffer vm)
        {
            return new JobOfferViewModel
            {
                Id = vm.Id,
                JobTitle = vm.JobTitle,
                CompanyName = vm.CompanyName,
                JobDescription = vm.JobDescription,
                ApplicationDeadline = vm.ApplicationDeadline,
                WorkStartDate = vm.WorkStartDate,
                Location = vm.Location,
                SalaryFrom = vm.SalaryFrom,
                SalaryTo = vm.SalaryTo,
                SalaryDescription = vm.SalaryDescription,
                ContractLength = vm.ContractLength,               
            };
        }
    }

    public class SalaryGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public SalaryGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ErrorMessage = "Value of \"salary to\" must be greater then value of \"salary from\"";
            var salaryTo = (int)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null) throw new ArgumentException("Property with this name not found");

            var salaryFrom = (int)property.GetValue(validationContext.ObjectInstance);

            if (salaryTo < salaryFrom)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }       
    }
    public class CheckDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;
            if (dt > DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Make sure your date is greater than today");
        }

    }
  
}
