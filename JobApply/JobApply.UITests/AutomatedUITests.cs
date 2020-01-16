using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Threading;
using Xunit;

namespace JobApply.UITests
{
    public class AutomatedUITests : IDisposable
    {

        private readonly IWebDriver driver;
        private readonly WebDriverWait waiter;
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
        public AutomatedUITests()
        {
            var dir = Directory.GetCurrentDirectory();
            ChromeOptions options = new ChromeOptions();
            driver = new ChromeDriver(dir);
            waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void GetJobOffers_WhenExecuted_ReturnsIndexView()
        {
            driver.Navigate().GoToUrl("https://localhost:44370/JobOffers/Index");
            Assert.Contains("Job Offer List", driver.PageSource);
        }

        [Fact]
        public void GetCompanyDetails_WhenUserNotLoggedIn_ReturnToComapniesIndex()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:44370");
            Thread.Sleep(1000);

            var companies = driver.FindElement(By.XPath("/html/body/nav/div/ul[1]/li[3]/a"));
            companies.Click();
            Thread.Sleep(1000);
            Assert.Contains("List of companies", driver.PageSource);

            var companyDetails = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[1]/td[6]/a"));
            companyDetails.Click();
            Thread.Sleep(1000);
            Assert.Contains("Company details", driver.PageSource);

            var goBack = driver.FindElement(By.XPath("/html/body/div/div[2]/a"));
            goBack.Click();
            Thread.Sleep(1000);
            Assert.Equal("https://localhost:44370/Companies", driver.Url);

        }

        [Fact]
        public void ApplyForJobOffer_WhenExecuted_NewJobApplicationsIsCreated()
        {
            string uri = "https://localhost:44370/JobOffers/Index";
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(uri);

            var countOffer = driver.FindElement(By.XPath("/html/body/div/div/div[1]/div/span"));
            int c = Int32.Parse(countOffer.Text);
            if (c < 1) return;
            Thread.Sleep(1000);

            var jobOffer = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[1]/td[1]/a"));
            jobOffer.Click();
            Thread.Sleep(1000);

            var applyNow = driver.FindElement(By.XPath("/html/body/div/div[3]/button"));
            applyNow.Click();
            Thread.Sleep(1000);

            if (FillForm(driver))
            {
                Thread.Sleep(10000);
                var submit = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[7]/input"));
                submit.Click();
            }

            Assert.Contains("Job Offer List", driver.PageSource);
        }

        public bool FillForm(IWebDriver driver)
        {
            string FirstName = "FirstNameTest";
            string LastName = "LastNameTest";
            string PhoneNumber = "123456789";
            string EmailAddress = "email@test.pl";
            string CvUrl = "my_cv.pdf";

            while(true)
            {
                try
                {

                    var firstName = driver.FindElement(By.Id("FirstName"));
                    firstName.Click();
                    firstName.SendKeys(FirstName);
                    Thread.Sleep(1000);

                    var lastName = driver.FindElement(By.Id("LastName"));
                    lastName.Click();
                    lastName.SendKeys(LastName);
                    Thread.Sleep(1000);

                    var phone = driver.FindElement(By.Id("PhoneNumber"));
                    phone.Click();
                    phone.SendKeys(PhoneNumber);
                    Thread.Sleep(1000);

                    var email = driver.FindElement(By.Id("EmailAddress"));
                    email.Click();
                    email.SendKeys(EmailAddress);
                    Thread.Sleep(1000);

                    var aggreement = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[5]/div/label/input"));
                    aggreement.Click();  
                    Thread.Sleep(1000);

                    var cv = driver.FindElement(By.Id("CvUrl"));
                    cv.Click();
                    cv.SendKeys(CvUrl);
                    Thread.Sleep(1000);

                    break;
                }
                catch { }
            }
            return true;
        }
    }
}
