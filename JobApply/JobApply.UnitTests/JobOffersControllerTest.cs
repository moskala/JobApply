using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobApply.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JobApply.EntityFramework;
using JobApply.Models;
using System.Collections.Generic;
using System;

namespace JobApply.UnitTests
{
    [TestClass]
    public class JobOffersControllerTest
    {
        DataContext context;
        static DbContextOptions<DataContext> options;

        [ClassInitialize]
        public static void GetOptions(TestContext testContext)
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "JobApplyDatabase")
                .Options;
        }

        [TestInitialize]
        public void PrepareDatabase()
        {       
            context = new DataContext(options);                                    
            context.Database.EnsureDeleted();
            context.JobOffers.Add(new JobOffer { Id = 1, JobTitle = "React Developer", CompanyName = "Facebook", Location = "London, UK" });
            context.JobOffers.Add(new JobOffer { Id = 2, JobTitle = "Film Producer", CompanyName = "HBO", Location = "London, UK" });
            context.SaveChanges();           
        }

        [TestMethod]
        public void JobOfferDetails_IdInDatabase_ViewResult()
        {
            JobOffersController jobOffersController = new JobOffersController(context);

            var response = jobOffersController.Details(1);

            Assert.IsInstanceOfType(response, typeof(ViewResult));
        }

        [TestMethod]
        public void JobOfferDetails_IdNotInDatabase_NotFoundResult()
        {
            JobOffersController jobOffersController = new JobOffersController(context);

            var response = jobOffersController.Details(5);

            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }

        [TestMethod]
        public void CreateJobOffer_ModelValid_RedirectToIndex()
        {
            JobOffersController jobOffersController = new JobOffersController(context);
            JobOfferViewModel jobOffer = new JobOfferViewModel() { Id = 4,};
            jobOffersController.ModelState.Clear();

            var result = jobOffersController.Create(jobOffer).GetAwaiter().GetResult();

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectToActionResult =  result as RedirectToActionResult;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod]
        public void CreateJobOffer_ModelIsNotValid_RedirectToViewResult()
        {
            JobOffersController jobOffersController = new JobOffersController(context);
            JobOfferViewModel jobOffer = new JobOfferViewModel();
            jobOffersController.ModelState.AddModelError("testError", "testError");

            var response = jobOffersController.Create(jobOffer).GetAwaiter().GetResult();

            Assert.IsInstanceOfType(response, typeof(ViewResult));

        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void JobOfferExists_NegativeValueAsId_ShouldThrowException()
        {
            JobOffersController jobOffersController = new JobOffersController(context);
            jobOffersController.JobOfferExists(-1);
        }

        [TestMethod]
        public void JobOfferExists_ValidIdOfExistingOffer_True()
        {
            JobOffersController jobOffersController = new JobOffersController(context);
            var result = jobOffersController.JobOfferExists(2);

            Assert.AreEqual(result, true);

        }

    }
}
