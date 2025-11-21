using NUnit.Framework;
using OpenQA.Selenium;
using qa_dotnet_cucumber.Pages;
using Reqnroll;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class LanguageUpgradeSteps
    {
        private readonly IWebDriver _driver;
        private readonly LanguageUpgradePages _languagePage;
        private string _currentTestLanguage = string.Empty;

        public LanguageUpgradeSteps(IWebDriver driver)
        {
            _driver = driver;
            _languagePage = new LanguageUpgradePages(driver);
        }

        [OneTimeSetUp]
        public void CleanAllLanguagesBeforeTests()
        {
            _languagePage.LoginToApplication();
            _languagePage.GoToLanguagesTab();
            _languagePage.DeleteAllLanguages(); 
        }

        [TearDown]
        public void CleanupAfterTest()
        {
            if (!string.IsNullOrEmpty(_currentTestLanguage))
            {
                _languagePage.DeleteLanguage(_currentTestLanguage);
                _currentTestLanguage = string.Empty;
            }
        }

        [Given("I am logged into the portal and on the Language tab")]
        public void GivenILoginAndGoToLanguageTab()
        {
            _languagePage.LoginToApplication();
            _languagePage.GoToLanguagesTab();
        }
    //    [When("I reset and add all languages")]
    //    public void WhenIResetAndAddAllLanguages()
    //    {
    //        var languages = new Dictionary<string, string>
    //{
    //    { "Spanish", "Native/Bilingual" },
    //    { "English", "Fluent" },
    //    { "French", "Basic" },
    //    { "Hindi", "Fluent" },
    //    { "Tamil", "Conversational" }
    //};

    //        _languagePage.ResetAndAddLanguages(languages);
    //    }

    //    [Then("I should see all languages in the table")]
    //    public void ThenIShouldSeeAllLanguages()
    //    {
    //        var languages = new[] { "Spanish", "English", "French", "Hindi", "Tamil" };
    //        foreach (var lang in languages)
    //        {
    //            Assert.That(_languagePage.IsLanguagePresent(lang), $"Language '{lang}' was not found in the table.");
    //        }
    //    }


        [When("I add a new language {string} with level {string}")]
        public void WhenIAddANewLanguageWithLevel(string language, string level)
        {
            
            if (_languagePage.IsLanguagePresent(language))
                _languagePage.DeleteLanguage(language);

            _languagePage.AddLanguage(language, level);
            _currentTestLanguage = language; 
        }

        [Then("I should see {string} in the language table")]
        public void ThenIShouldSeeLanguage(string language)
        {
            Assert.That(_languagePage.IsLanguagePresent(language), $"Language '{language}' was not found in the table.");
        }

        [When("I update language {string} to level {string}")]
        public void WhenIUpdateLanguageToLevel(string language, string newLevel)
        {
            
            if (!_languagePage.IsLanguagePresent(language))
            {
                _languagePage.AddLanguage(language, newLevel);
            }

            _languagePage.AddLanguage(language, newLevel); 
            _currentTestLanguage = language; 
        }

        [Then("I should see {string} updated to level {string}")]
        public void ThenIShouldSeeLanguageUpdatedToLevel(string language, string newLevel)
        {
            
            Assert.That(_languagePage.IsLanguagePresent(language),
                        $"Language '{language}' was not updated to '{newLevel}'.");
        }

        [When("I try to add an existing language {string}")]
        public void WhenITryToAddExistingLanguage(string language)
        {
            _languagePage.AddLanguage(language, "Fluent"); 
            Assert.Throws<InvalidOperationException>(() => _languagePage.AddLanguage(language, "Fluent"));
            _currentTestLanguage = language; 
        }

        [When("I try to delete a non-existing language {string}")]
        public void WhenITryToDeleteNonExistingLanguage(string language)
        {
            Assert.DoesNotThrow(() => _languagePage.DeleteLanguage(language));
        }
    }
}

