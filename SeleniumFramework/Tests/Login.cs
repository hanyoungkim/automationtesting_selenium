using OpenQA.Selenium;
using SeleniumFrameworkTests.pageObjects;
using SeleniumFrameworkTests.utilities;
using System.Collections.Immutable;

namespace SeleniumFrameworkTests.Tests
{
    [Order(1)]
    // [Parallelizable(ParallelScope.Children)] --> If I use this, the order of tests in the html report will be messed up. Need to fix later.
    public class Login : Base
    {
        [Order(1), TestCase("admin@sksolution.co.nz", "PAssword!@!@"), Category("Regression")]
        public void validLoginTest(String emailAddress, String password)
        {
            LoginPage loginPage = new LoginPage(getDriver());
            HomePage homePage = loginPage.validlogin(emailAddress, password);

            Assert.That(homePage.getLogoutButton().Displayed, Is.EqualTo(true));
        }

        [Order(2), TestCase("wrongemail@sksolution.co.nz", "PAssword!@!@"), Category("Regression")]
        public void invalidLoginWithInvalidEmailTest(String invalidEmail, String password)
        {
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.invalidlogin(invalidEmail, password);

            StringAssert.Contains("The login is invalid", loginPage.getInvalidLoginMessage().Text);
        }

        [Order(3), TestCase("admin@sksolution.co.nz", "wrongPassword!@!@"), Category("Regression")]
        public void invalidLoginWithInvalidPasswordTest(String username, String invalidPassword)
        {
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.invalidlogin(username, invalidPassword);

            StringAssert.Contains("The login is invalid", loginPage.getInvalidLoginMessage().Text);
        }
    }
}
