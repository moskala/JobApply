using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobApply.EntityFramework;
using JobApply.Models;

namespace JobApply.Controllers
{
    public class JobApplicationsController : Controller
    {
        private readonly DataContext _context;

        public JobApplicationsController(DataContext context)
        {
            _context = context;
        }

        // GET: JobApplications
        public async Task<IActionResult> Index()
        {
            var jobApplications = await _context.JobApplications.ToListAsync();
            var applications = new List<JobApplicationViewModel>();
            foreach(var app in jobApplications)
            {
                var jobOffer = await _context.JobOffers.FindAsync(app.OfferId);
                applications.Add(new JobApplicationViewModel()
                {
                    OfferId = app.OfferId,
                    ApplicationId = app.Id,
                    JobTitle = jobOffer.JobTitle,
                    CompanyName = jobOffer.CompanyName,
                    Location = jobOffer.Location
                });
            }
            return View(applications);
        }

        // GET: JobApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobApplication == null)
            {
                return NotFound();
            }
            JobApplicationViewModel model = jobApplication;
            var jobOffer = await _context.JobOffers.FirstOrDefaultAsync(m => m.Id == model.OfferId);
            if(jobOffer == null)
            {
                return NotFound();
            }
            model.JobTitle = jobOffer.JobTitle;
            model.CompanyName = jobOffer.CompanyName;
            model.Location = jobOffer.Location;           
            return View(model);
        }

        public async Task<IActionResult> ApplyForOffer(int? id)
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
            JobApplicationViewModel model = new JobApplicationViewModel()
            {
                OfferId = id.Value,
                JobTitle = jobOffer.JobTitle,
                CompanyName = jobOffer.CompanyName,
                Location = jobOffer.Location
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyForOffer([Bind("OfferId,JobTitle,CompanyName,Location,FirstName,LastName,PhoneNumber,EmailAddress,ContactAgreement,CvUrl")] JobApplicationViewModel application)
        {
            if (ModelState.IsValid)
            {
                JobApplication jobApplication = application;
                application.Created = DateTime.Now;
                _context.Add(jobApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(application);
            }       
        }

        // POST: JobApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobApplication = await _context.JobApplications.FindAsync(id);
            _context.JobApplications.Remove(jobApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobApplicationExists(int id)
        {
            return _context.JobApplications.Any(e => e.Id == id);
        }
    }
}
