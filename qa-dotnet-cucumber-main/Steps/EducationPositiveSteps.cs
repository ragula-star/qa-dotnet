using System;
using qa_dotnet_cucumber.Pages;
using OpenQA.Selenium;
using Reqnroll;
using NUnit.Framework;
using System.Threading;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class EducationPositiveSteps
    {
        private readonly IWebDriver _driver;
        private readonly EducationPage _educationPage;
        private readonly ScenarioContext _scenarioContext;

        public EducationPositiveSteps(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _educationPage = new EducationPage(driver);
            _scenarioContext = scenarioContext;
        }
        [Given("I am logged into the portal")]
        public void GivenILoggedIntoThePortal()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.LoginWithValidUser();
        }

       
        [Given("I navigate to the Education tab")]
        public void GivenINavigateToTheEducationTab()
        {
            _educationPage.GoToEducationtab();
        }

        
        [Given("I delete all existing education records")]
        public void GivenIDeleteAllExistingEducationRecords()
        {
            _educationPage.DeleteAllEducation();
        }

        [When("I add the following Education")]
        [When("I attempt to add Education with missing required fields")]
        [When("I attempt to add Education without filling any fields")]
        [When("I attempt to add Education with special characters")]
        [When("I attempt to add Education with extremely long input")]
        public void WhenIAddEducation(Table table)
        {
            foreach (var row in table.Rows)
            {
                _educationPage.AddEducation(
                    row["university Name"],
                    row["country of College\\University"],
                    row["title"],
                    row["Degree"],
                    row["Year of graduate"]
                );
                
                if (!_scenarioContext.ContainsKey("AddedEducation"))
                {
                    _scenarioContext["AddedEducation"] = new List<(string university, string countryCollege, string title, string degree, string year)>();
                }

                var addedList = (List<(string university, string countryCollege, string title, string degree, string year)>)_scenarioContext["AddedEducation"];
                addedList.Add((
                    row["university Name"],
                    row["country of College\\University"],
                    row["title"],
                    row["Degree"],
                    row["Year of graduate"]
                ));
            }
        }

        
        [When("I attempt to add the same education record again")]
        public void WhenIAttemptToAddDuplicateEducation(Table table)
        {
            foreach (var row in table.Rows)
            {
                _educationPage.AddEducation(
                    row["university Name"],
                    row["country of College\\University"],
                    row["title"],
                    row["Degree"],
                    row["Year of graduate"]
                );
            }
        }

       
        [Then("all education entries should be added successfully")]
        [Then ("all educations invalid entries should be added successfully")]
        [Then ("all destructive education entries should be added successfully")]
        public void ThenAllEducationEntriesShouldBeAddedSuccessfully(Table table)
        {
            var expectedUniversities = table.Rows.Select(r => r["university Name"]).ToList();
            var actualUniversities = _educationPage.GetAllEducation();

            Assert.That(actualUniversities, Is.EquivalentTo(expectedUniversities), "Education entries mismatch.");
        }

        
        [Then("an error message should be displayed for missing required fields")]
        [Then("an error message should be displayed for missing or empty fields")]
        public void ThenErrorMessageForMissingOrEmptyFields()
        {
            var errorMessage = _educationPage.GetErrorMessage();
            Assert.That(!string.IsNullOrEmpty(errorMessage), "Expected an error message for missing or empty fields.");
        }

       
        [Then("an error message for duplicate entry should be displayed")]
        public void ThenDuplicateEntryMessageShouldBeDisplayed()
        {
            var duplicateErrorMessage = _educationPage.AssertDuplicateMessage();
            Assert.That(!string.IsNullOrEmpty(duplicateErrorMessage), "Expected a duplicate entry error message.");
        }

       
        [Then("an error message should be displayed for invalid input")]
        [Then("an error message should be displayed or input should be truncated")]
        [Then("an error message should be displayed")]
        public void ThenErrorMessageForInvalidInput()
        {
            var allEducation = _educationPage.GetAllEducation();
            foreach (var uni in allEducation)
            {
                Assert.That(uni.Length < 200, "University name is too long");
                Assert.That(!uni.Any(c => "!@#$%^&*()".Contains(c)), "University name has invalid characters");
            }
        }
    }
}
