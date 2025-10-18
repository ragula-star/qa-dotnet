using OpenQA.Selenium;
using Reqnroll;
using NUnit.Framework;
using qa_dotnet_cucumber.Pages;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class SkillSteps
    {
        private readonly IWebDriver _driver;
        private readonly SkillPage _skillPage;
        private readonly LoginPage _loginPage;

        public SkillSteps(IWebDriver driver)
        {
            _driver = driver;
            _skillPage = new SkillPage(_driver);
            _loginPage = new LoginPage(_driver);
        }

        //[Given(@"I logged into the portal")]
        //public void GivenILoggedIntoThePortal()
        //{
        //    _driver.Navigate().GoToUrl("http://localhost:5003/Home");
        //    _loginPage.signIn();
        //    _loginPage.Login("ragulau4@gmail.com", "Ragula123@");
        //}

        [When(@"I click on the Skills tab")]
        public void WhenIClickOnTheSkillsTab()
        {
            _skillPage.GoToSkillsTab();
        }

        [Then(@"I add the following skills:")]
        public void ThenIAddTheFollowingSkills(Table table)
        {
            foreach (var row in table.Rows)
            {
                string skill = row["Skill"];
                string level = row["Skill Level"];
                _skillPage.AddSkill(skill, level);
                Assert.That(_skillPage.IsSkillPresent(skill, level),
                    $"Skill '{skill}' with level '{level}' was not added correctly.");
            }
        }

        [When(@"I edit the skill ""(.*)"" to have level ""(.*)""")]
        public void WhenIEditTheSkillToHaveLevel(string skill, string level)
        {
            _skillPage.EditSkill(skill, level);
        }
        [Then(@"I delete the following skills:")]
        public void ThenIDeleteTheFollowingSkills(Table table)
        {
            foreach (var row in table.Rows)
            {
                string skill = row["Skill"];

                if (_skillPage.IsSkillExists(skill))
                {
                    _skillPage.DeleteSkill(skill);
                    Console.WriteLine($"Deleted skill: {skill}");
                }
                else
                {
                    Console.WriteLine($"Skill '{skill}' not found — skipping deletion.");
                }
            }
        }

    }
}

