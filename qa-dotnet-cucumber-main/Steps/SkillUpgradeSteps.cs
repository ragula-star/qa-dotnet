using NUnit.Framework;
using OpenQA.Selenium;
using qa_dotnet_cucumber.Pages;
using Reqnroll;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class SkillUpgradeSteps
    {
        private readonly IWebDriver _driver;
        private readonly SkillUpgradePages _skillPage;
        private readonly ScenarioContext _scenarioContext;

        public SkillUpgradeSteps(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _skillPage = new SkillUpgradePages(driver);
            _scenarioContext = scenarioContext;
        }

        [Given("I am logged into the portal and on the Skills tab")]
        public void GivenILoginAndGoToSkillsTab()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.LoginWithValidUser();
            _skillPage.GoToSkillsTab();
            _skillPage.DeleteAllSkills(); 
        }
        [When("I try to add a skill with \"(.*)\" and \"(.*)\"")]
        public void WhenITryToAddSkillWithData(string skill, string level)
        {
           
            _skillPage.AddSkill(skill, level);
        }

        [When("I add the following skills:")]
        public void WhenIAddTheFollowingSkills(Table table)
        {
            var addedSkills = new List<string>();

            foreach (var row in table.Rows)
            {
                string skill = row["Skill"];
                string level = row["Level"];
                _skillPage.AddSkill(skill, level);
                addedSkills.Add(skill);
            }

            _scenarioContext["AddedSkills"] = addedSkills;
        }


        //[When("I try to add a skill with \"(.*)\" and \"(.*)\"")]
        //public void WhenITryToAddSkillWithData(string skill, string level)
        //{
        //    //_skillPage.GoToSkillsTab();
        //    _skillPage.AddSkill(skill, level);
        //}

        [Then("all skills should be added successfully")]
        public void ThenAllSkillsShouldBeAdded(Table table)
        {
            var skillNames = table.Rows.Select(r => r["Skill"]).ToArray();
            Assert.That(_skillPage.AreSkillsAdded(skillNames), Is.True, "Not all skills were added successfully.");
        }

        //[Then("I should see error message {string}")]
        //public void ThenIShouldSeeErrorMessage(string expectedMessage)
        //{
        //    string actualMessage = _skillPage.GetErrorMessage();
        //    Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Expected error message '{expectedMessage}', but got '{actualMessage}'.");
        //}

        [Then("I should see skill error message {string}")]
        public void ThenIShouldSeeSkillErrorMessage(string expectedMessage)
        {
            string actualMessage = _skillPage.GetErrorMessage();
            Assert.That(actualMessage, Is.EqualTo(expectedMessage),
                $"Expected error message '{expectedMessage}', but got '{actualMessage}'.");
        }


        [Then("no new skill should be added")]
        public void ThenNoNewSkillShouldBeAdded()
        {
            Assert.That(_skillPage.IsNoNewSkillAdded(), Is.True, "No new skill should be added.");
        }

    }
}
