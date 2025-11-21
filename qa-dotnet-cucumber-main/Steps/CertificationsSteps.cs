using System;
using qa_dotnet_cucumber.Pages;
using OpenQA.Selenium;
using Reqnroll;
using NUnit.Framework;
using System.Threading;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class CertificationsSteps
    {
        private readonly IWebDriver _driver;
        private readonly CertificationsPage _certPage;
        private readonly ScenarioContext _scenarioContext;

        public CertificationsSteps(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _certPage = new CertificationsPage(driver);
            _scenarioContext = scenarioContext;
        }
        //[Given("I am logged into the portal")]
        //public void GivenILoggedIntoThePortal()
        //{
        //    var loginPage = new LoginPage(_driver);
        //    loginPage.LoginWithValidUser();
        //}

        [Given(@"I navigate to the Certifications tab")]
        public void GivenINavigateToTheCertificationsTab()
        {
            _certPage.GoToCertificationsTab();
            _certPage.DeleteAllCertifications();
            
        }

        [When(@"I add the following certification")]
        public void WhenIAddTheFollowingCertification(Table table)
        {
            
            var row = table.Rows[0];
            _certPage.AddCertification(row["Certificate Name"], row["Certified From"], row["Year"]);
        }

        [When(@"I add the following certifications")]
        [When(@"I attempt to add the same certification again")]
        public void WhenIAddTheFollowingCertifications(Table table)
        {
            foreach (var row in table.Rows)
            {
                _certPage.AddCertification(row["Certificate Name"], row["Certified From"], row["Year"]);
            }
        }

        [When(@"I edit the certification to the following values")]
        public void WhenIEditTheCertification(Table table)
        {
            var row = table.Rows[0];
            _certPage.EditCertification(
                oldName: "AWS Certified Developer",
                newName: row["Certificate Name"],
                newFrom: row["Certified From"],
                newYear: row["Year"]
            );
        }


        //[When(@"I attempt to add the same certification again")]
        //public void WhenIAttemptToAddDuplicateCertification()
        //{
        //   
        //}

        [When(@"I attempt to add a certification without filling any fields")]
        public void WhenIAttemptToAddCertificationWithEmptyFields(Table table)
        {
            _certPage.AddCertification("", "", "");
        }

        [When(@"I attempt to add a certification with invalid characters")]
        public void WhenIAttemptToAddCertificationWithInvalidCharacters(Table table)
        {
            var row = table.Rows[0];
            _certPage.AddCertification(row["Certificate Name"], row["Certified From"], row["Year"]);
        }

        [Then(@"the certification should be added successfully")]
        public void ThenTheCertificationShouldBeAddedSuccessfully()
        {
            var allCerts = _certPage.GetAllCertifications();
            Assert.That(allCerts.Count, Is.GreaterThan(0), "No certification was added!");
        }

        [Then(@"all certifications should be added successfully")]
        public void ThenAllCertificationsShouldBeAddedSuccessfully()
        {
            var allCerts = _certPage.GetAllCertifications();
            Assert.That(allCerts.Count, Is.GreaterThanOrEqualTo(2), "Not all certifications were added!");
        }

        //[Then(@"the portal should show an error for duplicate certification entry")]
        //public void ThenThePortalShouldShowAnErrorForDuplicateCertificationEntry()
        //{
        //    var message = _certPage.GetDuplicateMessage();
        //    Assert.That(message, Does.Contain("Duplicated data"));  
        //}

        [Then(@"the certification should be updated successfully")]
        public void ThenCertificationUpdated()
        {
            var certs = _certPage.GetAllCertifications();
            Assert.That(certs, Does.Contain("AWS Architect Expert"), "Edit did not update certification!");
        }

        //[Then(@"the portal should show an error for missing certification fields")]
        //public void ThenThePortalShouldShowAnErrorForMissingCertificationFields()
        //{
        //    var message = _certPage.GetErrorMessage();
        //    Assert.That(message, Does.Contain("Please enter Certification Name, Certification From and Certification Year")); 
        //}

        [Then(@"the certification should be added successfully in portal")]
        public void ThenTheCertificationShouldBeAddedSuccessfullyInPortal()
        {
            var allCerts = _certPage.GetAllCertifications();
            Assert.That(allCerts.Count, Is.GreaterThan(0), "No certification was added!");
        }
    }
}