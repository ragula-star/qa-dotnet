using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers; 

namespace qa_dotnet_cucumber.Pages
{
    public class LanguageEditPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

       
        private readonly By LanguagesTab = By.XPath("//a[text()='Languages']");
        private By GetEditButton(string language) =>
        By.XPath($"//td[normalize-space(text())='{language}']/following-sibling::td//span[i[contains(@class,'write icon')]]");

        private By GetSaveButton(string language) =>
        By.XPath($"//td[normalize-space(text())='{language}']/following-sibling::td//input[@type='button' and @value='Update']");

        private By LevelDropdown = By.XPath("//select[@name='level']");

        public LanguageEditPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void GoToLanguagesTab()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(LanguagesTab)).Click();
        }

        public void EditLanguage(string language, string level)
        {
            
            _wait.Until(ExpectedConditions.ElementToBeClickable(GetEditButton(language))).Click();
            IWebElement levelElement = _wait.Until(ExpectedConditions.ElementIsVisible(LevelDropdown));
            var selectElement = new SelectElement(levelElement);
            selectElement.SelectByText(level);

            var updateButton = _wait.Until(driver =>
            {
                var btn = driver.FindElement(By.XPath(
                    $"//div[@class='fields'][.//input[@name='name' and normalize-space(@value)='{language}']]//input[@value='Update']"));
                return (btn.Displayed && btn.Enabled) ? btn : null;
            });

            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", updateButton);
            updateButton.Click();
        }

        public void ThenIShouldSeeUpdateConfirmation()
        {
            
            IWebElement popup = _wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//div[text()='This language is already added to your language list.']")
            ));
            string popupText = popup.Text;

            Assert.That(popupText, Is.EqualTo("This language is already added to your language list."),
                "Expected popup message not shown.");
        }
        public bool IsLanguagePresent(string language, string level)
        {
            try
            {
                
                var xpath = $"//table//tr[normalize-space(td[1])='{language}' and normalize-space(td[2])='{level}']";
                _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
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

            _wait.Until(ExpectedConditions.AlertIsPresent());
            _driver.SwitchTo().Alert().Accept();
        }

        public bool IsLanguageNotPresent(string language)
        {
            try
            {
                var xpath = $"//td[normalize-space(text())='{language}']";
                _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(xpath)));
                return true;
            }
            catch
            {
                return false;
            }
        }




    }
}
