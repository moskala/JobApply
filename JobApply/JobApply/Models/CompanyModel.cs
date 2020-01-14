using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApply.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ContactEmail { get; set; }
        public DateTime FoundationDate { get; set; }

    }
}
