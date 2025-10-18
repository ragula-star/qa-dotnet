using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using qa_dotnet_cucumber.Pages;
using System; 

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class LanguageEditSteps
    {
        private readonly IWebDriver _driver;
        private readonly LanguageEditPage _languageEditPage;
        private readonly LanguagePages _languagePages;
        private readonly LoginPage _loginPage; 

        public LanguageEditSteps(IWebDriver driver)
        {
            _driver = driver;
            _languageEditPage = new LanguageEditPage(_driver);
            _loginPage = new LoginPage(_driver);
            _languagePages = new LanguagePages(_driver);
        }

        //[Given(@"I logged into the portal")]
        //public void GivenILoggedIntoThePortal()
        //{
        //    _driver.Navigate().GoToUrl("http://localhost:5003/Home");
        //    _loginPage.signIn();
        //    _loginPage.Login("ragulau4@gmail.com", "Ragula123@");
        //    Thread.Sleep(3000);
        //}

        //[When(@"I click on the Languages tab")]
        //public void WhenIClickOnTheLanguagesTab()
        //{
        //    _languagePages.GoToLanguagesTab();
        //}

        [When(@"I edit the language ""(.*)"" to have level ""(.*)""")]
        public void WhenIEditTheLanguageToHaveLevel(string language, string level)
        {
            _languageEditPage.EditLanguage(language, level);
        }

        [Then(@"I should see ""(.*)"" listed with level ""(.*)""")]
        public void ThenIShouldSeeLanguageListedWithLevel(string language, string level)
        {
            
            _languageEditPage.ThenIShouldSeeUpdateConfirmation();
        }
        [Then(@"I should see ""(.*)"" listed with level ""(.*)"" in my list")]
        public void ThenIShouldSeeLanguageInList(string language, string level)
        {
            bool exists = _languagePages.IsLanguagePresent(language, level);
            Assert.That(exists, Is.True, $"Language '{language}' with level '{level}' was not found in the list.");
        }
        [When(@"I delete the language ""(.*)""")]
        public void WhenIDeleteTheLanguage(string language)
        {
            _languagePages.DeleteLanguage(language);
        }
        [Then(@"I should not see ""(.*)"" in my language list")]
        public void ThenIShouldNotSeeLanguage(string language)
        {
            bool exists = _languagePages.IsLanguagePresent(language, ""); 
            Assert.That(exists, Is.False, $"Language '{language}' was not deleted successfully.");
        }




    }
}