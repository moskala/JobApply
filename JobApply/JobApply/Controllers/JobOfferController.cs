using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApply.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobApply.Controllers
{
    [Route("[controller]/[action]")]
    public class JobOfferController : Controller
    {
        private static List<JobOffer> _jobOffers = new List<JobOffer>()
        {
            new JobOffer{Id=1, JobTitle="Backend Developer"},
            new JobOffer{Id=2, JobTitle="Frontend Developer"},
            new JobOffer{Id=3, JobTitle="Manager"},
            new JobOffer{Id=4, JobTitle="Teacher"},
            new JobOffer{Id=5, JobTitle="Cook"},
        };
        public IActionResult Index()
        {
            return View(_jobOffers);
        }

        public ActionResult Details(int id)
        {
            var job = _jobOffers.FirstOrDefault((x) => x.Id == id);
            return View(job);
        }
    }
}