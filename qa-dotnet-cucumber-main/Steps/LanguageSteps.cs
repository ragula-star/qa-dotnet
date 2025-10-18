using qa_dotnet_cucumber.Pages;
using OpenQA.Selenium;
using Reqnroll;
using NUnit.Framework;
using System.Threading;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class LanguageSteps
    {
        private readonly IWebDriver _driver;
        private readonly LanguagePages _languagePages;
        private readonly LoginPage _loginPage;

        public LanguageSteps(IWebDriver driver)
        {
            _driver = driver;
            _languagePages = new LanguagePages(_driver);
            _loginPage = new LoginPage(_driver);
        }

        [Given(@"I logged into the portal")]
        public void GivenILoggedIntoThePortal()
        {
            _driver.Navigate().GoToUrl("http://localhost:5003/Home");
            _loginPage.signIn();
            _loginPage.Login("ragulau4@gmail.com", "Ragula123@");
            Thread.Sleep(3000);
        }

        [When(@"I click on the Languages tab")]
        public void WhenIClickOnTheLanguagesTab()
        {
            _languagePages.GoToLanguagesTab();
        }

        [When(@"I add the following languages:")]
        public void WhenIAddTheFollowingLanguages(Table table)
        {
            foreach (var row in table.Rows)
            {
                string language = row["Language"];
                string level = row["Level"];
                _languagePages.AddLanguage(language, level);
            }
        }

        [When(@"I should see ""(.*)"" listed with ""(.*)""")]
        public void WhenIShouldSeeListedWith(string language, string level)
        {
            bool isPresent = _languagePages.IsLanguagePresent(language, level);
            Assert.That(isPresent, Is.True,
              $"Expected to see language '{language}' with level '{level}', but it was not found.");

        }

        [Then(@"I should see all languages listed")]
        public void ThenIShouldSeeAllLanguagesListed(Table table)
        {
            foreach (var row in table.Rows)
            {
                string language = row["Language"];
                string level = row["Level"];
                Assert.That(_languagePages.IsLanguagePresent(language, level), Is.True,
                $"Language '{language}' with level '{level}' was not found in the list.");
            }

            Assert.That(_languagePages.IsAddNewButtonDisplayed(), Is.False,
                        "Add New button should not be visible after 4 languages.");
        }
        [Then(@"I should see {string} listed with {string}")]
        public void ThenIShouldSeeListedWith(string language, string level)
        {
            bool isPresent = _languagePages.IsLanguagePresent(language, level);
            Assert.That(isPresent, Is.True, $"Expected to see language '{language}' with level '{level}', but it was not found.");
        }

        [Then(@"I delete the {string}")]
        public void ThenIDeleteThe(string language)
        {
            _languagePages.DeleteLanguage(language);
            Assert.That(_languagePages.IsLanguagePresent(language), Is.False, $"Language '{language}' was not deleted.");
        }

    }
}

