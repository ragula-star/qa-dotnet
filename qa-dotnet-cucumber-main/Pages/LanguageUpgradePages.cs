using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;

namespace qa_dotnet_cucumber.Pages
{
    public class LanguageUpgradePages
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly LoginPage _loginPage;

        public LanguageUpgradePages(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            _loginPage = new LoginPage(driver);
        }

        private readonly By LanguageRows = By.XPath("//table[@class='ui fixed table']//tr/td[1]"); 
        private readonly By LanguageTab = By.XPath("//a[@data-tab='first' and text()='Languages']");
        private readonly By AddNewButton = By.XPath("//th[@class='right aligned']//div[contains(@class,'teal button') and text()='Add New']");
        private readonly By LanguageInput = By.XPath("//input[@placeholder='Add Language']");
        private readonly By LevelDropdown = By.XPath("//select[@name='level']");
        private readonly By AddButton = By.XPath("//input[@value='Add']");
        private readonly By LanguageTable = By.XPath("//table[@class='ui fixed table']");
        private readonly By DeleteIcon = By.XPath(".//span[contains(@class,'button')]/i[@class='remove icon']");
        private readonly By ErrorPopup = By.XPath("//div[contains(@class,'ns-box-inner')]");

        public string GetErrorMessage()
        {
            try
            {
                return _wait.Until(ExpectedConditions.ElementIsVisible(ErrorPopup)).Text;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;
            }
        }

        public void LoginToApplication()
        {
            _loginPage.LoginWithValidUser();
        }
        public void GoToLanguagesTab()
        {
            var languageTab = By.XPath("//a[@data-tab='first' and text()='Languages']");
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(languageTab)).Click();
        }
        public void AddLanguage(string language, string level)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(LanguageTab)).Click();

            
            if (IsLanguagePresent(language))
            {
                DeleteLanguage(language);
               _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath($"//td[text()='{language}']")));
            }

            _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewButton)).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(LanguageInput)).Clear();
            _driver.FindElement(LanguageInput).SendKeys(language);
            _driver.FindElement(LevelDropdown).SendKeys(level);
            _driver.FindElement(AddButton).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//td[text()='{language}']")));
        }


        public bool IsLanguagePresent(string language)
        {
            var table = _wait.Until(ExpectedConditions.ElementIsVisible(LanguageTable));
            return table.Text.Contains(language);
        }

        public void DeleteLanguage(string language)
        {
            
            var languageRow = new WebDriverWait(_driver, TimeSpan.FromSeconds(20))
                .Until(ExpectedConditions.ElementIsVisible(
                    By.XPath($"//td[contains(text(),'{language}')]/parent::tr")));

             var deleteButton = languageRow.FindElement(By.XPath(".//span[contains(@class,'button')]/i[@class='remove icon']"));
             deleteButton.FindElement(By.XPath("./parent::span")).Click();
            
            new WebDriverWait(_driver, TimeSpan.FromSeconds(20))
                .Until(ExpectedConditions.InvisibilityOfElementLocated(
                    By.XPath($"//td[contains(text(),'{language}')]/parent::tr")));
        }

        public void DeleteAllLanguages()
        {
            try
            {
                var rows = _driver.FindElements(By.XPath("//table[@class='ui fixed table']//tr[td]"));
                foreach (var row in rows)
                {
                    var language = row.FindElement(By.XPath("./td[1]")).Text;
                    DeleteLanguage(language); 
                }
            }
            catch (NoSuchElementException)
            {
                
            }
        }
        public void AddAndVerifyLanguage(string language, string level)
        {
            AddLanguage(language, level);
            Assert.That(IsLanguagePresent(language), $"Language '{language}' was not found after adding.");
        }

        public void CleanupLanguage(string language)
        {
            if (IsLanguagePresent(language))
            {
                DeleteLanguage(language);
            }
        }
        public void ResetAndAddLanguages(Dictionary<string, string> languages)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(LanguageTab)).Click();
             DeleteAllLanguages(); 

            foreach (var lang in languages)
            {
                _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewButton)).Click();
                _wait.Until(ExpectedConditions.ElementIsVisible(LanguageInput)).Clear();
                _driver.FindElement(LanguageInput).SendKeys(lang.Key);
                _driver.FindElement(LevelDropdown).SendKeys(lang.Value);
                _driver.FindElement(AddButton).Click();
              _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//td[text()='{lang.Key}']")));
            }
        }
        public bool IsNoNewLanguageAdded()
        {
            var rows = _driver.FindElements(By.XPath("//table[@class='ui fixed table']//tr[td]"));
            return rows.Count == 0;
        }
    }
}

