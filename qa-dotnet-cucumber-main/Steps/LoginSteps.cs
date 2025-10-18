using OpenQA.Selenium;
using Reqnroll;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using qa_dotnet_cucumber.Pages;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly LoginPage _loginPage;
        private readonly NavigationHelper _navigationHelper;

        public LoginSteps(LoginPage loginPage, NavigationHelper navigationHelper)
        {
            _loginPage = loginPage;
            _navigationHelper = navigationHelper;
        }

        [Given("I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            _navigationHelper.NavigateTo("/Home");
            _loginPage.signIn();
            Assert.That(_loginPage.IsAtLoginPage(), Is.True, "Should be on the login page");
        }

        [When("I enter valid credentials")]
        public void WhenIEnterValidCredentials()
        {
            _loginPage.Login("ragulau4@gmail.com", "Ragula123@");
        }

        [When("I enter an invalid username and valid password")]
        public void WhenIEnterAnInvalidUsernameAndValidPassword()
        {
            _loginPage.Login("invaliduser", "Ragula123@");
        }

        [When("I enter a valid username and invalid password")]
        public void WhenIEnterAValidUsernameAndInvalidPassword()
        {
            _loginPage.Login("ragulau4@gmail.com", "");
        }

        [When("I enter empty credentials")]
        public void WhenIEnterEmptyCredentials()
        {
            _loginPage.Login("", "");
        }

        [When("I enter username with wrong case")]
        public void WhenIEnterUserNameWithWrongCase()
        {
            _loginPage.Login("ragulau4@gmai@.com", "Ragula123@");
        }
        [When("I enter password with wrong case")]
        public void WhenIEnterPasswordWithWrongCase()
        {
            _loginPage.Login("ragulau4@gmail.com", "Ragula123_");
        }

        [When("I enter username with special characters")]
        public void WhenIEnterUsernameWithSpecialCharacters()
        {
            _loginPage.Login("!@#$%^&*", "Ragula123@");
        }
        [When("I enter password with special characters")]
        public void WhenIEnterPasswordWithSpecialCharacters()
        {
            _loginPage.Login("ragulau4@gmail.com", "!@#$%^&*");
        }
        [When("I enter a very long username")]
        public void WhenIEnteraVeryLongUsername()
        {
            string newlonguser = new string('a', 500);
            _loginPage.Login("newlonguser", "Ragula123@");
        }
        [When("I enter a very long password")]
        public void WhenIEnterALongPassword()
        {
            string password = new string('b', 600);
            _loginPage.Login("ragulau4@gmail.com", "ragula123@");
        }
        [When("I enter username with leading and trailing spaces")]
        public void WhenIEnterUsernameWithLeadingAndTrailingSpaces()
        {
            _loginPage.Login("  ragulau4@gmail.com  ", "Ragula123@");
        }
        [When("I enter password with leading and trailing spaces")]
        public void WhenIEnterPasswordWithLeadingAnsSpaces()
        {
            _loginPage.Login("ragulau4@gmail.com", "  Ragula123@  ");
        }

        [Then("I should see the secure area")]
        public void ThenIShouldSeeTheSecureArea()
        {
            var successMessage = _loginPage.GetSuccessMessage();
            Assert.That(successMessage, Does.Contain("Mars Logo"), "Should see successful login message");
        }

        [Then("I should see an email error message")]
        public void ThenIShouldSeeAnEmailErrorMessage()
        {
            var EmailMessage = _loginPage.GetEmailMessage();
            Assert.That(EmailMessage, Does.Contain("Please enter a valid email address"), "No Error Alert Message");
        }
        [Then("I should see an email password error message")]
        public void ThenIShouldSeeAnEmailPasswordErrorMessage()
        {
            var emailAlert = _loginPage.GetEmailAlert();
            Assert.That(emailAlert, Does.Contain("Please enter a valid email address"), "No Error Alert Message");

            var passwordAlert = _loginPage.GetPasswordAlert();
            Assert.That(passwordAlert, Does.Contain("Password must be at least 6 characters"), "No Error Alert Message");

        }
        [Then("I should see an email valid alert")]
        public void ThenIShouldSeeAnEmailValidAlert()
        {
            var EmaiAlert = _loginPage.GetEmailAlert();
            Assert.That(EmaiAlert, Does.Contain("Please enter a valid email address"), "No Error Alert Message");
        }
        [Then("I should see an email wrong alert")]
        public void ThenIshouldSeeAnEmailWrongAlert()
        {
            var emaillalert = _loginPage.GetEmailAlert();
        }
        [Then("I should see an invalid email message")]
        public void ThenIShouldSeeAnInvalidEmailMessage()
        {
            var alertEmail = _loginPage.GetEmailAlert();
            Assert.That(alertEmail, Does.Contain("Please enter a valid email address"), "No Error Alert Message");
        }
        [Then("I should see an enter valid email message")]
        public void ThenIShouldSeeANEnterValidEmailMessage()
        {
            var invalidemail = _loginPage.GetEmailAlert();
            Assert.That(invalidemail, Does.Contain("Please enter a valid email address"), "No Error Alert Message");
        }
    }
}
