using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using qa_dotnet_cucumber.Pages;
using FluentAssertions;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class EducationUpgradeSteps
    {
        private readonly IWebDriver _driver;
        private readonly EducationPage _educationPage;
        private readonly ScenarioContext _context;



        public EducationUpgradeSteps(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _educationPage = new EducationPage(driver);
            _context = scenarioContext;
        }
        //[Given("I am logged into the portal")]
        //public void GivenILoggedIntoThePortal()
        //{
        //    var loginPage = new LoginPage(_driver);
        //    loginPage.LoginWithValidUser();
        //}


        //[Given("I navigate to the Education tab")]
        //public void GivenINavigateToTheEducationTab()
        //{
        //    _educationPage.GoToEducationtab();
        //}

        [Given(@"I am on the Education tab")]
        public void GivenIAmOnTheEducationTab()
        {
            _educationPage.GoToEducationtab();
        }
        [When(@"I delete all education entries")]
        public void WhenIDeleteAllEducationEntries()
        {
            _educationPage.DeleteAllEducation();
        }
        private string GetJsonPath()
        {
            var projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            return Path.Combine(projectDir, "Tests", "education.json"); 
        }

        [When(@"I add all education entries from JSON except empty fields")]
        public void WhenIAddAllEducationEntriesFromJSONExceptEmptyFields()
        {
            var jsonText = File.ReadAllText(GetJsonPath());
            var jObject = JObject.Parse(jsonText);
            var educationArray = jObject["education"].ToArray();

            foreach (var edu in educationArray)
            {
                if (string.IsNullOrEmpty((string)edu["university"])) continue;

                _educationPage.AddEducation(
                    (string)edu["university"],
                    (string)edu["country"],
                    (string)edu["title"],
                    (string)edu["degree"],
                    (string)edu["year"]
                );
            }
        }

        [When(@"I add empty education entries from JSON")]
        public void WhenIAddEmptyEducationEntriesFromJSON()
        {
            var jsonText = File.ReadAllText(GetJsonPath());
            var jObject = JObject.Parse(jsonText);
            var educationArray = jObject["education"].ToArray();

            foreach (var edu in educationArray)
            {
                if (!string.IsNullOrEmpty((string)edu["university"])) continue;

                _educationPage.AddEducation(
                    (string)edu["university"],
                    (string)edu["country"],
                    (string)edu["title"],
                    (string)edu["degree"],
                    (string)edu["year"]
                );
            }
        }

        [Then(@"I should see all added universities in my profile")]
        public void ThenIShouldSeeAllAddedUniversitiesInMyProfile()
        {
            var addedUniversities = _educationPage.GetAllEducation();
            Assert.That(addedUniversities, Is.Not.Empty, "No universities were added.");
        }

        [Then(@"I should see validation error messages for empty fields")]
        public void ThenIShouldSeeValidationErrorMessagesForEmptyFields()
        {
            var message = _educationPage.GetErrorMessage();
            Assert.That(!string.IsNullOrEmpty(message), Is.True, "Expected validation error for empty fields.");
        }

        [When(@"I edit the last education entry with JSON data ""(.*)""")]
        public void WhenIEditTheLastEducationEntryWithJsonData(string scenario)
        {
            var jsonText = File.ReadAllText(GetJsonPath());
            var jObject = JObject.Parse(jsonText);
            var edu = jObject["education"].First(x => x["scenario"].ToString() == scenario);

            _educationPage.EditLastEducation(
                (string)edu["university"],
                (string)edu["country"],
                (string)edu["title"],
                (string)edu["degree"],
                (string)edu["year"]
            );
        }


        [Then(@"I should see the updated university in my profile")]
        public void ThenIShouldSeeTheUpdatedUniversityInMyProfile()
        {
            var addedUniversities = _educationPage.GetAllEducation();
            Assert.That(addedUniversities.Last(), Is.EqualTo("Edited University"));
        }

        [Then(@"I should see no education entries in my profile")]
        public void ThenIShouldSeeNoEducationEntriesInMyProfile()
        {
            var entries = _educationPage.GetAllEducation();
            Assert.That(entries.Count, Is.EqualTo(0), "Education entries were not deleted.");
        }
    }
}
