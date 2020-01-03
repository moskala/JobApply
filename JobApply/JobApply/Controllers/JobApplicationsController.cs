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
            var applications = new List<UserApplicationsListViewModel>();
            foreach(var app in jobApplications)
            {
                var jobOffer = await _context.JobOffers.FindAsync(app.OfferId);
                applications.Add(new UserApplicationsListViewModel()
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

            return View(jobApplication);
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
            var newApplication = new JobApplication();
            newApplication.OfferId = id.Value;
            ViewData["JobTitle"] = $"Application for {jobOffer.JobTitle} in {jobOffer.CompanyName}";
            return View(newApplication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyForOffer([Bind("OfferId,FirstName,LastName,PhoneNumber,EmailAddress,ContactAgreement,CvUrl")] JobApplication jobApplication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(jobApplication);
            }       
        }

        // GET: JobApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(jobApplication);
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
