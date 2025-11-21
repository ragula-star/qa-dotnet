using NUnit.Framework;
using OpenQA.Selenium;
using qa_dotnet_cucumber.Pages;
using Reqnroll;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class LanguageUpgradeNegativeSteps
    {
        private readonly IWebDriver _driver;
        private readonly LanguageUpgradePages _languagePage;
        private readonly LanguagesUpgradeNegativePages _negativePage;

        public LanguageUpgradeNegativeSteps(IWebDriver driver)
        {
            _driver = driver;
            _languagePage = new LanguageUpgradePages(driver);
            _negativePage = new LanguagesUpgradeNegativePages(driver);
        }

        [Given("I am logged into the portal and on the Language tab for negative tests")]
        public void GivenILoginAndGoToLanguageTabForNegative()
        {
            _languagePage.LoginToApplication();
            _negativePage.DeleteAllLanguagesIfExist();
            _negativePage.GoToLanguagesTab();
        }

        [When("I try to add a language with \"(.*)\" and \"(.*)\"")]
        public void WhenITryToAddLanguageWithData(string language, string level)
        {
            _negativePage.ClickAddNewWithoutData();

            if (!string.IsNullOrEmpty(language))
            {
                _negativePage.EnterLanguage(language);
            }

            if (!string.IsNullOrEmpty(level))
            {
                _negativePage.SelectLevel(level);
            }

            if (!string.IsNullOrEmpty(language) && !string.IsNullOrEmpty(level))
            {
                
            }
            else if (!string.IsNullOrEmpty(language))
            {
                _negativePage.ClickAddButtonWithoutLevel();
            }
            else if (!string.IsNullOrEmpty(level))
            {
                _negativePage.ClickAddButtonWithoutLanguage();
            }
            else
            {
                _negativePage.ClickAddNewWithoutData();
            }
        }
        [Then("I should see error message {string}")]
        public void ThenIShouldSeeErrorMessage(string expectedMessage)
        {
            string actualMessage = _languagePage.GetErrorMessage();
            Assert.That(actualMessage, Is.EqualTo(expectedMessage),
                $"Expected error message '{expectedMessage}', but got '{actualMessage}'");
        }


        [Then("no new language should be added")]
        public void ThenNoNewLanguageShouldBeAdded()
        {
            Assert.That(_languagePage.IsNoNewLanguageAdded(), Is.True, "No new language should be added.");

        }
    }
}

