using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace qa_dotnet_cucumber.Pages
{
    public class SkillPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By SkillsTab = By.XPath("//a[text()='Skills']");
        private readonly By AddNewButton = By.XPath("//div[@class='ui teal button' and text()='Add New']");
        private readonly By SkillInput = By.XPath("//input[@name='name']");
        private readonly By SkillLevelDropdown = By.XPath("//select[@name='level']");
        private readonly By AddButton = By.XPath("//input[@value='Add']");

        public SkillPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void GoToSkillsTab()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(SkillsTab)).Click();
        }

        public void AddSkill(string skill, string level)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewButton)).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(SkillInput)).SendKeys(skill);

            var dropdown = _wait.Until(ExpectedConditions.ElementIsVisible(SkillLevelDropdown));
            var select = new SelectElement(dropdown);
            select.SelectByText(level);

            _wait.Until(ExpectedConditions.ElementToBeClickable(AddButton)).Click();
        }

        public bool IsSkillPresent(string skill, string level)
        {
            try
            {
                var xpath = $"//table//tr[td[1][normalize-space()='{skill}'] and td[2][normalize-space()='{level}']]";
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
            
            var editButton = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(
                $"//td[normalize-space(text())='{skill}']/following-sibling::td//span[i[contains(@class,'write icon')]]"
            )));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", editButton);
            editButton.Click();
            var dropdown = _wait.Until(ExpectedConditions.ElementIsVisible(SkillLevelDropdown));
            var select = new SelectElement(dropdown);
            select.SelectByText(level);

            var updateButton = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(
                $"//span[@class='buttons-wrapper']/input[@value='Update']"
            )));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", updateButton);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", updateButton); 
        }


        //public void DeleteSkill(string skill)
        //{
        //    var deleteButton = _wait.Until(driver =>
        //    {
        //        var btn = driver.FindElement(By.XPath(
        //            $"//td[normalize-space(text())='{skill}']/following-sibling::td//span[i[contains(@class,'remove icon')]]"));
        //        return (btn.Displayed && btn.Enabled) ? btn : null;
        //    });

        //    ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", deleteButton);
        //    deleteButton.Click();

        //    // Accept alert if confirmation appears
        //    _wait.Until(ExpectedConditions.AlertIsPresent());
        //    _driver.SwitchTo().Alert().Accept();
        //}
        public string GetSkillLevel(string skill)
        {
            try
            {
                var xpath = $"//td[normalize-space(text())='{skill}']/following-sibling::td[1]";
                var element = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                return element.Text.Trim();
            }
            catch
            {
                return null;
            }
        }

        public bool IsSkillExists(string skill)
        {
            try
            {
                var xpath = $"//td[normalize-space(text())='{skill}']";
                _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void DeleteSkill(string skill)
        {
            try
            {
                var deleteButton = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(
                    $"//td[normalize-space(text())='{skill}']/following-sibling::td//span[i[contains(@class,'remove icon')]]"
                )));

                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", deleteButton);
                deleteButton.Click();

                _wait.Until(ExpectedConditions.AlertIsPresent());
                _driver.SwitchTo().Alert().Accept();
                 System.Threading.Thread.Sleep(1500);

                _wait.Until(driver =>
                {
                    var elements = driver.FindElements(By.XPath($"//td[normalize-space(text())='{skill}']"));
                    return elements.Count == 0;
                });
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Timeout while deleting skill: {skill}. It might have already been removed.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Skill '{skill}' not found ,skipping.");
            }
        }


    }
}

