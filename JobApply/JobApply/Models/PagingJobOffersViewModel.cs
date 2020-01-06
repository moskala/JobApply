using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApply.Models
{
    public class PagingJobOffersViewModel
    {
        public IEnumerable<JobOfferViewModel> JobOffers { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
