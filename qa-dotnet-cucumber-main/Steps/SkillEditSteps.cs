using qa_dotnet_cucumber.Pages;
using OpenQA.Selenium;
using NUnit.Framework;
using Reqnroll;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class SkillEditSteps
    {
        private readonly IWebDriver _driver;
        private readonly SkillEditPage _skillEditPage;

        public SkillEditSteps(IWebDriver driver)
        {
            _driver = driver;
            _skillEditPage = new SkillEditPage(_driver);
        }

        //[When(@"I click on the Skills tab")]
        //public void WhenIClickOnTheSkillsTab()
        //{
        //    _skillEditPage.GoToSkillsTab();
        //}

        //[When(@"I edit the skill ""(.*)"" to have level ""(.*)""")]
        //public void WhenIEditTheSkillToHaveLevel(string skill, string newLevel)
        //{
        //    _skillEditPage.EditSkill(skill, newLevel);
        //}

        //[Then(@"I should see skill ""(.*)"" listed with ""(.*)""")]
        //public void ThenIShouldSeeSkillListedWith(string skill, string level)
        //{
        //    bool isPresent = _skillEditPage.IsSkillPresent(skill, level);
        //    Assert.That(isPresent, Is.True, $"Expected to see skill '{skill}' with level '{level}', but it was not found.");
        //}
    }
}

