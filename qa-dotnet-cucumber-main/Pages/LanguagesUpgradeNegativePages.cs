using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;


namespace qa_dotnet_cucumber.Pages
{
    public class LanguagesUpgradeNegativePages
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By LanguageTab = By.XPath("//a[@data-tab='first' and text()='Languages']");
        private readonly By AddNewButton = By.XPath("//div[contains(@class,'ui teal button') and normalize-space()='Add New']");
        private readonly By LanguageInput = By.XPath("//input[@placeholder='Add Language']");
        private readonly By LevelDropdown = By.XPath("//select[@name='level']");
        private readonly By AddButton = By.XPath("//input[@value='Add']");
        private readonly By LanguageTableRows = By.XPath("//table[@class='ui fixed table']//tr[td]");

        public LanguagesUpgradeNegativePages(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void GoToLanguagesTab()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(LanguageTab)).Click();
        }
        public void DeleteAllLanguagesIfExist()
        {
            var deleteIcons = _driver.FindElements(By.XPath("//i[@class='remove icon']"));
            foreach (var icon in deleteIcons)
            {
                try
                {
                    _wait.Until(ExpectedConditions.ElementToBeClickable(icon)).Click();
                    _driver.SwitchTo().Alert().Accept(); 
                   
                }
                catch (NoAlertPresentException)
                {
                    
                }
            }
        }
        public void ClickAddNewWithoutData()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewButton)).Click();
        }

        public void EnterLanguage(string language)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(LanguageInput)).Clear();
            _driver.FindElement(LanguageInput).SendKeys(language);
        }

        public void SelectLevel(string level)
        {
            var dropdown = new SelectElement(_driver.FindElement(LevelDropdown));
            dropdown.SelectByText(level);
        }

        public void ClickAddButtonWithoutLevel()
        {
            _driver.FindElement(AddButton).Click();
        }

        public void ClickAddButtonWithoutLanguage()
        {
            _driver.FindElement(AddButton).Click();
        }

        public bool IsNoNewLanguageAdded()
        {
            var rows = _driver.FindElements(LanguageTableRows);
            return rows.Count == 0;
        }
    }
}

