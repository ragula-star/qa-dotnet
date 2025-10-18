using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace qa_dotnet_cucumber.Pages
{
    public class LanguagePages
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public IWebDriver Driver => _driver;

        public LanguagePages(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        private By LanguagesTab => By.XPath("//a[text()='Languages']");
        
        private By AddNewButton => By.XPath("//div[contains(@class,'ui teal button') and text()='Add New']");
        private By LanguageInput => By.Name("name");
        private By LevelDropdown => By.Name("level");
        private By AddButton => By.XPath("//input[@value='Add']");

        public void GoToLanguagesTab()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(LanguagesTab)).Click();
        }

        public void AddLanguage(string language, string level)
        {
            if (IsLanguagePresent(language, level))
                return; 

            _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewButton)).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(LanguageInput)).SendKeys(language);

            var levelSelect = new SelectElement(_driver.FindElement(LevelDropdown));
            levelSelect.SelectByText(level);

            _driver.FindElement(AddButton).Click();

            
            try
            {
                var popup = By.XPath("//div[contains(text(),'already have this language')]");
                _wait.Until(ExpectedConditions.ElementIsVisible(popup));
                _driver.FindElement(By.XPath("//button[text()='OK']")).Click();
            }
            catch (WebDriverTimeoutException)
            {
                
            }
             _wait.Until(d => IsLanguagePresent(language, level));
        }

        public bool IsLanguagePresent(string language, string level = "")
        {
            try
            {
                string xpath = string.IsNullOrEmpty(level)
                    ? $"//td[normalize-space(text())='{language}']"
                    : $"//td[normalize-space(text())='{language}']/following-sibling::td[text()='{level}']";

                return _driver.FindElement(By.XPath(xpath)).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

       public bool IsAddNewButtonDisplayed()
        {
            try
            {
                return _driver.FindElement(AddNewButton).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public void DeleteLanguage(string language)
        {
            
            var deleteButton = _wait.Until(driver =>
            {
                var btn = driver.FindElement(By.XPath(
                    $"//td[normalize-space(text())='{language}']/following-sibling::td//span[i[contains(@class,'remove icon')]]"));
                return (btn.Displayed && btn.Enabled) ? btn : null;
            });
           ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", deleteButton);
           deleteButton.Click();

            try
            {
                var confirmBtn = By.XPath("//div[contains(@class,'ui modal')]//button[text()='Yes']");
                _wait.Until(ExpectedConditions.ElementToBeClickable(confirmBtn)).Click();
            }
            catch (WebDriverTimeoutException)
            {
                
            }
           _wait.Until(d => !IsLanguagePresent(language, "any")); 
        }


    }
}
