using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApply.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Display(Name = "Company Name")]
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Display(Name = "Office city")]
        [Required]
        public string City { get; set; }
        [Display(Name = "Office country")]
        [Required]
        public string Country { get; set; }
        [Display(Name = "Contact email")]
        [Required]
        [EmailAddress]
        public string ContactEmail { get; set;}

        [Display(Name = "Founded")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime FoundationDate { get; set; }

        public static implicit operator Company(CompanyModel model)
        {
            return new Company
            {
                Id = model.Id,
                Name = model.Name,
                City = model.City,
                Country = model.Country,
                ContactEmail = model.ContactEmail,
                FoundationDate = model.FoundationDate
            };
        }

        public static implicit operator CompanyModel(Company model)
        {
            return new CompanyModel
            {
                Id = model.Id,
                Name = model.Name,
                City = model.City,
                Country = model.Country,
                ContactEmail = model.ContactEmail,
                FoundationDate = model.FoundationDate
            };
        }

    }

    public class CompanyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Office city")]
        public string City { get; set; }
        public string Country { get; set; }
        public string ContactEmail { get; set; }
        public DateTime FoundationDate { get; set; }      

    }


    
}
