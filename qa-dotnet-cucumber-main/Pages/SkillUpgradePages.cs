using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace qa_dotnet_cucumber.Pages
{
    public class SkillUpgradePages
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public SkillUpgradePages(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private readonly By SkillsTab = By.XPath("//a[@data-tab='second' and text()='Skills']");
        private readonly By AddNewButton = By.XPath("//div[@class='ui teal button' and text()='Add New']");
        private readonly By SkillInput = By.XPath("//div[@class='five wide field']//input[@placeholder='Add Skill']");
        private readonly By LevelDropdown = By.XPath("//select[@name='level']");
        private readonly By AddButton = By.XPath("//input[@value='Add']");
        private readonly By SkillRows = By.XPath("//table[@class='ui fixed table']//tr/td[1]");
        private readonly By DeleteIcon = By.XPath(".//span[contains(@class,'button')]/i[@class='remove icon']");
        private readonly By ErrorPopup = By.XPath("//div[contains(@class,'ns-box-inner')]");

        public void GoToSkillsTab()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(SkillsTab)).Click();
            
            
        }

        public void AddSkill(string skill, string level)
        {
            
            var addNew = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='ui teal button' and text()='Add New']")));
            addNew.Click();

            var input = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Add Skill']")));
            input.Clear();
            input.SendKeys(skill);

            
            //var dropdown = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//select[@name='level']")));
            //new SelectElement(dropdown).SelectByText(level);
            if (!string.IsNullOrWhiteSpace(level))
            {
                var dropdown = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//select[@name='level']")));
                new SelectElement(dropdown).SelectByText(level);
            }
           _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@value='Add']"))).Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            _wait.Until(d => AreSkillsAdded(new[] { skill }));
        }


        public void DeleteSkillByName(string skillName)
        {
            var rows = _driver.FindElements(SkillRows);
            foreach (var row in rows)
            {
                if (row.Text.Equals(skillName, StringComparison.OrdinalIgnoreCase))
                {
                    row.FindElement(DeleteIcon).Click();
                    new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
                        .Until(d => !row.Displayed);
                    break;
                }
            }
        }

        public void DeleteAllSkills()
        {
            while (true)
            {
                var skillRows = _driver.FindElements(By.XPath("//table[@class='ui fixed table']/tbody/tr"));
                if (skillRows.Count == 0)
                    break;

                int initialCount = skillRows.Count;
                var firstRow = skillRows[0];
                var deleteIcon = firstRow.FindElement(By.XPath(".//i[contains(@class,'remove icon')]"));
                deleteIcon.Click();

                try
                {
                    var confirmButton = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Yes']")));
                    confirmButton.Click();
                }
                catch (WebDriverTimeoutException)
                {
                    
                }

              _wait.Until(d => d.FindElements(By.XPath("//table[@class='ui fixed table']/tbody/tr")).Count < initialCount);

            }
        }

        public string GetErrorMessage()
        {
            try
            {
                return _wait.Until(ExpectedConditions.ElementIsVisible(ErrorPopup)).Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool AreSkillsAdded(params string[] skills)
        {
            var currentSkills = _driver.FindElements(SkillRows).Select(r => r.Text).ToList();
            return skills.All(s => currentSkills.Contains(s));
        }

        public bool IsNoNewSkillAdded() => !_driver.FindElements(SkillRows).Any();
    }
}

