using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobApply.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool ContactAgreement { get; set; }
        public string CvUrl { get; set; }     
        public DateTime Created { get; set; }

        [ForeignKey("JobOffer")]
        public int OfferId { get; set; }
        public JobOffer JobOffer { get; set; }

    }
}
