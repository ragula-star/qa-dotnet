using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace qa_dotnet_cucumber.Pages
{
    public class SkillEditPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By SkillsTab = By.XPath("//a[text()='Skills']");
        private readonly By LevelDropdown = By.XPath(".//select[@name='level']"); 
        private By GetEditButton(string skill) =>
            By.XPath($"//td[normalize-space(text())='{skill}']/following-sibling::td//i[contains(@class,'write icon')]");

        public SkillEditPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void GoToSkillsTab()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(SkillsTab)).Click();
        }

        public void EditSkill(string skill, string newLevel)
        {
            
            var editBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(GetEditButton(skill)));
            editBtn.Click();
             var rowDropdown = _wait.Until(driver =>
                driver.FindElement(By.XPath($"//td[normalize-space(text())='{skill}']/following-sibling::td//select[@name='level']"))
            );
            var select = new SelectElement(rowDropdown);
            select.SelectByText(newLevel);

            var updateBtn = _wait.Until(driver =>
                driver.FindElement(By.XPath($"//td[normalize-space(text())='{skill}']/following-sibling::td//input[@value='Update']"))
            );

            updateBtn.Click();
        }

        public bool IsSkillPresent(string skill, string level)
        {
            try
            {
                var xpath = $"//table//tr[normalize-space(td[1])='{skill}' and normalize-space(td[2])='{level}']";
                _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

