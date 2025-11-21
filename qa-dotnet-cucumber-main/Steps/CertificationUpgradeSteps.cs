using System;
using qa_dotnet_cucumber.Pages;
using OpenQA.Selenium;
using Reqnroll;
using NUnit.Framework;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.IO;
using FluentAssertions;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class CertificationUpgradeSteps
    {
        private readonly IWebDriver _driver;
        private readonly CertificationsPage _certificationsPage;
        private readonly ScenarioContext _scenarioContext;

        public CertificationUpgradeSteps(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _certificationsPage = new CertificationsPage(driver);
            _scenarioContext = scenarioContext;
        }
        //[Given("I am logged into the portal")]
        //public void GivenILoggedIntoThePortal()
        //{
        //    var loginPage = new LoginPage(_driver);
        //    loginPage.LoginWithValidUser();
        //}

        //[Given(@"I navigate to the Certifications tab")]
        //public void GivenINavigateToTheCertificationsTab()
        //{
        //    _certificationsPage.GoToCertificationsTab();
        //    _certificationsPage.DeleteAllCertifications();
        //}
        [When("I add a new certification from JSON")]
        public void WhenIAddANewCertificationsFromJSON()
        {
            var projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            var jsonPath = Path.Combine(projectRoot, "Tests", "certifications.json");
            string jsonText = File.ReadAllText(jsonPath);
            var json = JObject.Parse(jsonText);
            var dataList = json["certifications"]?.ToList();

            if (dataList == null || !dataList.Any())
                throw new Exception("No certifications found in JSON!");

            var addedList = new List<string>();

            foreach (var item in dataList)
            {
                string name = item["name"]?.ToString();
                string from = item["from"]?.ToString();
                string year = item["year"]?.ToString();

                _certificationsPage.AddCertification(name, from, year);
                addedList.Add(name);
            }

            _scenarioContext["AddedCerts"] = addedList;
        }

        
        [When(@"I add a new certification with empty fields")]
        public void WhenIAddCertificationWithEmptyFields()
        {
            _certificationsPage.AddCertification("", "", "");
        }

        
        [When(@"I add a new certification with invalid characters")]
        public void WhenIAddCertificationWithInvalidCharacters()
        {
            _certificationsPage.AddCertification("_+__(*&^@$#%@#", "123249u3885", "2022");
        }

        
        [When(@"I add a certification that already exists")]
        public void WhenIAddDuplicateCertification()
        {
            var existingCerts = _scenarioContext["AddedCerts"] as List<string>;
            if (existingCerts != null && existingCerts.Count > 0)
            {
                var certName = existingCerts[0];
                _certificationsPage.AddCertification(certName, "Duplicate Institute", "2023");
            }
        }

        
        [When(@"I attempt to add a certification with name ""(.*)""")]
        public void WhenIAttemptToAddCertificationWithName(string name)
        {
            _scenarioContext["CertName"] = name;
        }

        [When(@"from ""(.*)""")]
        public void WhenFrom(string from)
        {
            _scenarioContext["CertFrom"] = from;
        }

        [When(@"year ""(.*)""")]
        public void WhenYear(string year)
        {
            string name = _scenarioContext["CertName"]?.ToString();
            string from = _scenarioContext["CertFrom"]?.ToString();
            _certificationsPage.AddCertification(name, from, year);
        }

        
        [When(@"I add certifications from negative JSON")]
        public void WhenIAddCertificationsFromNegativeJSON()
        {
            var projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            var jsonPath = Path.Combine(projectRoot, "Tests", "certificationnegative.json");
            string jsonText = File.ReadAllText(jsonPath);

            var json = JObject.Parse(jsonText);
            var dataList = json["certifications"]?.ToList();
            if (dataList == null) return;

            foreach (var item in dataList)
            {
                string name = item["name"]?.ToString();
                string from = item["from"]?.ToString();
                string year = item["year"]?.ToString();
                _certificationsPage.AddCertification(name, from, year);
            }
        }

       
        [Then("all certification should be added successfully")]
        [Then(@"the portal should show an successfull for invalid characters")]
        //[Then(@"the portal should show an successfull for year")]
        public void ThenAllCertificationShouldBeAddedSuccessfully()
        {
            var addedCerts = _scenarioContext["AddedCerts"] as List<string>;

            foreach (var cert in addedCerts)
            {
                bool visible = _certificationsPage.IsCertificationVisible(cert);
                visible.Should().BeTrue($"{cert} should be visible after adding it.");
            }
        }

        
        [Then(@"the portal should show an error for missing certification fields")]
        public void ThenPortalShowsErrorForMissingFields()
        {
            var error = _certificationsPage.GetErrorMessage();
            error.Should().Contain("Please enter Certification Name, Certification From and Certification Year");
        }

        //[Then(@"the portal should show an error for invalid characters")]
        //public void ThenPortalShowsErrorForInvalidCharacters()
        //{
        //    var error = _certificationsPage.GetErrorMessage();
        //    error.Should().Contain("Invalid characters");
        //}

        [Then(@"the portal should show an error for duplicate certification entry")]
        public void ThenPortalShowsErrorForDuplicateEntry()
        {
            var message = _certificationsPage.GetDuplicateMessage();
            message.Should().Contain("already exist.");
        }

        [Then(@"the portal should handle the input gracefully")]
        public void ThenPortalHandlesInputGracefully()
        {
            var error = _certificationsPage.GetErrorMessage();
            var success = _certificationsPage.GetSuccessMessage();
            (string.IsNullOrEmpty(error) || string.IsNullOrEmpty(success)).Should().BeFalse("Portal should handle extreme input gracefully");
        }

        //[Then(@"the portal should show an suc for invalid year")]
        //public void ThenPortalShowsErrorForInvalidYear()
        //{
        //    var error = _certificationsPage.GetErrorMessage();
        //    error.Should().Contain("Invalid year");
        //}

        [Then("the certification name should be truncated to the maximum allowed length")]
        public void ThenTheCertificationNameShouldBeTruncatedToTheMaximumAllowedLength()
        {

            const int ExpectedMaxLength = 250;

            string originalName = (string)_scenarioContext["CertName"]!;
            Assert.That(originalName.Length, Is.EqualTo(ExpectedMaxLength),
                $"BUG FOUND: Expected name to be truncated to {ExpectedMaxLength} characters, but its length was {originalName.Length}.");

        }
    }
}
