using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;

namespace qa_dotnet_cucumber.Pages
{
    public class SkillCheckPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By SkillsTab = By.XPath("//a[text()='Skills']");
        private By GetEditButton(string skill) =>
            By.XPath($"//td[normalize-space(text())='{skill}']/following-sibling::td//i[contains(@class,'write icon')]");
        private By GetDeleteButton(string skill) =>
            By.XPath($"//td[normalize-space(text())='{skill}']/following-sibling::td//i[contains(@class,'remove icon')]");
        private By LevelDropdown = By.XPath("//select[@name='level']");

        public SkillCheckPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void GoToSkillsTab()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(SkillsTab)).Click();
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

        public void EditSkill(string skill, string level)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(GetEditButton(skill))).Click();
            var dropdown = _wait.Until(ExpectedConditions.ElementIsVisible(LevelDropdown));
            var select = new SelectElement(dropdown);
            select.SelectByText(level);

            var updateBtn = _driver.FindElement(By.XPath($"//td[normalize-space(text())='{skill}']/following-sibling::td//input[@value='Update']"));
            updateBtn.Click();
        }

        public void DeleteSkill(string skill)
        {
            var deleteBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(GetDeleteButton(skill)));
            deleteBtn.Click();
            _wait.Until(ExpectedConditions.AlertIsPresent());
            _driver.SwitchTo().Alert().Accept();
        }

        public bool IsSkillNotPresent(string skill)
        {
            try
            {
                var xpath = $"//td[normalize-space(text())='{skill}']";
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
