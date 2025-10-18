using qa_dotnet_cucumber.Pages;
using OpenQA.Selenium;
using Reqnroll;
using NUnit.Framework;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class SkillCheckSteps
    {
        private readonly IWebDriver _driver;
        private readonly SkillCheckPage _skillCheckPage;

        public SkillCheckSteps(IWebDriver driver)
        {
            _driver = driver;
            _skillCheckPage = new SkillCheckPage(_driver);
        }

        //[When(@"I click on the Skills tab")]
        //public void WhenIClickOnTheSkillsTab()
        //{
        //    _skillCheckPage.GoToSkillsTab();
        //}

        [Then(@"I should see skill ""(.*)"" listed with ""(.*)""")]
        public void ThenIShouldSeeSkillListedWithLevel(string skill, string level)
        {
            bool isPresent = _skillCheckPage.IsSkillPresent(skill, level);
            Assert.That(isPresent, Is.True, $"Expected to see skill '{skill}' with level '{level}', but it was not found.");
        }

        //[When(@"I edit the skill ""(.*)"" to have level ""(.*)""")]
        //public void WhenIEditSkillToHaveLevel(string skill, string level)
        //{
        //    _skillCheckPage.EditSkill(skill, level);
        //}

        [Then(@"I delete the skill ""(.*)""")]
        public void ThenIDeleteSkill(string skill)
        {
            _skillCheckPage.DeleteSkill(skill);
        }

        [Then(@"I should not see the skill ""(.*)"" in my skill list")]
        public void ThenIShouldNotSeeSkill(string skill)
        {
            bool notPresent = _skillCheckPage.IsSkillNotPresent(skill);
            Assert.That(notPresent, Is.True, $"Skill '{skill}' was not deleted successfully.");
        }
    }
}

