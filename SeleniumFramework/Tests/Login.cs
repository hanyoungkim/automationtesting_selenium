using OpenQA.Selenium;
using SeleniumFramework.pageObjects;
using SeleniumFramework.utilities;
using System.Collections.Immutable;

namespace SeleniumFramework.Tests
{
    [Order(1)]
    public class Login : Base
    {
        [Order(1), TestCase("lucas.selenium.classroom", "PAssword!@!@"), Category("Regression")]
        public void ValidLoginTest(String username, String password)
        {
            LoginPage loginPage = new LoginPage(getDriver());
            HomePage homePage = loginPage.validlogin(username, password);

            homePage.waitForLucas101Display();

            Assert.That(homePage.getCreateOrJoinClassButton().Enabled, Is.True);
        }

        [Order(2), TestCase("lucas.selenium.classroom", "PAssword#$#$"), Category("Regression")]
        public void InvalidLoginTest(String username, String password)
        {
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.invalidlogin(username, password);

            StringAssert.Contains("Wrong password", loginPage.getWrongPasswordMessage().Text);
        }
    }
}
