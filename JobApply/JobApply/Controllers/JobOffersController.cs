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
    public class JobOffersController : Controller
    {
        private readonly DataContext _context;

        public JobOffersController(DataContext context)
        {
            _context = context;
        }

        // GET: JobOffers
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


        // GET: JobOffers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: JobOffers/Edit/5
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

        // POST: JobOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: JobOffers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var jobOffer = await _context.JobOffers
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (jobOffer == null)
        //    {
        //        return NotFound();
        //    }
        //    JobOfferViewModel model = jobOffer;
        //    return View(model);
        //}

        // POST: JobOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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

        private bool JobOfferExists(int id)
        {
            return _context.JobOffers.Any(e => e.Id == id);
        }
    }
}
