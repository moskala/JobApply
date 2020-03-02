using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobApply.EntityFramework;
using JobApply.Models;
using Microsoft.AspNetCore.Authorization;

namespace JobApply.Controllers
{
    [Route("JobOffers")]
    public class JobOffersController : Controller
    {
        private readonly DataContext _context;

        public JobOffersController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list of job offers.
        /// </summary>
        /// <returns></returns>
        [Route("Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new List<JobOfferViewModel>();
            var jobOffers = await _context.JobOffers.ToListAsync();
            foreach(var offer in jobOffers)
            {               
                model.Add(offer);
            }
            return View(model);
        }

        /// <summary>
        /// Get one job offer
        /// </summary>
        /// <param name="id">Id of job offer</param>
        /// <returns>Details view for job offer</returns>
        [Route("Details/{id}")]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOffer = _context.JobOffers
                .FirstOrDefault(m => m.Id == id);
            if (jobOffer == null)
            {
                return NotFound();
            }
            JobOfferViewModel model = jobOffer;
            model.JobApplications = _context.JobApplications.Where(o => o.OfferId == id).ToList();
            return View(model);
        }


        /// <summary>
        /// Get form to create new job offer
        /// </summary>
        /// <returns>Form view</returns>
        [Route("Create")]
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post new job offer model. If model is valid it is saved to database.
        /// </summary>
        /// <param name="jobOfferCreate">Model with data for new job offer</param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,JobTitle,CompanyName,JobDescription,ApplicationDeadline,WorkStartDate,Location,SalaryFrom,SalaryTo,SalaryDescription,ContractLength")] JobOfferViewModel jobOfferCreate)
        {
            if (ModelState.IsValid)
            {
                JobOffer jobOffer = jobOfferCreate;
                _context.Add(jobOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobOfferCreate);
        }

        /// <summary>
        /// Get action to load one job offer to edit form.
        /// </summary>
        /// <param name="id">Id of a job offer</param>
        /// <returns>View with form to edit job offer</returns>
        [Route("Edit/{id}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOffer = await _context.JobOffers.FindAsync(id);
            if (jobOffer == null)
            {
                return NotFound();
            }
            JobOfferViewModel model = jobOffer;
            return View(model);
        }

        /// <summary>
        /// Post to update job offer. If model is valid data is saved to database. 
        /// </summary>
        /// <param name="id">Id of job offer</param>
        /// <param name="jobOfferEdit">Model with data to save.</param>
        /// <returns></returns>
        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobTitle,CompanyName,JobDescription,ApplicationDeadline,WorkStartDate,Location,SalaryFrom,SalaryTo,SalaryDescription,ContractLength")] JobOfferViewModel jobOfferEdit)
        {
            if (id != jobOfferEdit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                JobOffer jobOffer = jobOfferEdit;
                try
                {
                    _context.Update(jobOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobOfferExists(jobOffer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(jobOfferEdit);
        }

        /// <summary>
        /// Delete action for one job offer.
        /// </summary>
        /// <param name="id">Id of job offer to delete</param>
        /// <returns></returns>
        [Route("Delete/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applications = _context.JobApplications.Where(a => a.OfferId == id).ToArray();
            for(int i = 0; i < applications.Length; ++i)
            {
                _context.JobApplications.Remove(applications[i]);
            }
            var jobOffer = await _context.JobOffers.FindAsync(id);
            _context.JobOffers.Remove(jobOffer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool JobOfferExists(int id)
        {
            if (id < 0) throw new ArgumentException("Id is smaller than 0");
            return _context.JobOffers.Any(e => e.Id == id);
        }

        /// <summary>
        /// Get method to load only small number of Job Applications for one job offer.
        /// </summary>
        /// <param name="OfferId">Id of job offer for applications</param>
        /// <param name="pageNo">Number of current page to load, default 1</param>
        /// <param name="pageSize">Number of element to load on current page, default 4</param>
        /// <returns></returns>
        [Route("GetJobApplications")]
        [HttpGet]
        public PagingJobApplicationsViewModel GetJobApplications(int OfferId, int pageNo = 1, int pageSize = 4)
        {
            int totalPage, totalRecord;
            var applications = _context.JobApplications.ToList();
            var applicationsList = new List<JobApplicationListViewModel>();
            foreach(var app in applications)
            {
                JobApplicationListViewModel item = app;
                applicationsList.Add(item);
            }
            totalRecord = applications.Count();
            totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            var record = (from u in applicationsList
                          orderby u.FirstName, u.LastName
                          select u).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            PagingJobApplicationsViewModel empData = new PagingJobApplicationsViewModel
            {
                JobApplications = record,
                TotalPage = totalPage
            };

            return empData;
        }

        /// <summary>
        /// Get method to load only small number of Job Offers.
        /// </summary>
        /// <param name="pageNo">Value of current page to load</param>
        /// <param name="pageSize">Number of element to load on current page, default 4<</param>
        /// <returns></returns>
        [Route("GetJobOffers")]
        [HttpGet]
        public PagingJobOffersViewModel GetJobOffers(int pageNo = 1, int pageSize = 4)
        {
            int totalPage, totalRecord;
            var jobOffers = _context.JobOffers.ToList();
            var jobOffersView = new List<JobOfferViewModel>();
            foreach (var offer in jobOffers)
            {
                JobOfferViewModel item = offer;
                jobOffersView.Add(item);
            }
            totalRecord = jobOffersView.Count();
            totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            var record = (from u in jobOffersView
                          orderby u.Id
                          select u).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            PagingJobOffersViewModel data = new PagingJobOffersViewModel
            {
                JobOffers = record,
                CurrentPage = pageNo,
                TotalPage = totalPage,
            };

            return data;
        }
       
    }
}
