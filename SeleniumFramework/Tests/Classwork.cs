using OpenQA.Selenium;
using SeleniumFramework.pageObjects;
using SeleniumFramework.utilities;
using System.Collections.Immutable;

namespace SeleniumFramework.Tests
{
    [Order(3)]
    public class Classwork : Base
    {
        [Order(1), TestCase("lucas.selenium.classroom", "PAssword!@!@"), Category("Regression")]
        public void ValidLoginTest(String username, String password)
        {
            LoginPage loginPage = new LoginPage(getDriver());
            HomePage homePage = loginPage.validlogin(username, password);

            homePage.waitForLucas101Display();

            Assert.That(homePage.getCreateOrJoinClassButton().Enabled, Is.True);
        }
    }
}
