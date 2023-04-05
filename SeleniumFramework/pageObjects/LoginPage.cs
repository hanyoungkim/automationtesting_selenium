using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumFrameworkTests.utilities;

namespace SeleniumFrameworkTests.pageObjects
{
    internal class LoginPage : Base
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='user']")]
        private IWebElement TbEmail;

        [FindsBy(How = How.XPath, Using = "//input[@id='pass']")]
        private IWebElement TbPassword;

        [FindsBy(How = How.XPath, Using = "//button[@id='login_submit']")]
        private IWebElement BLogIn;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'The login is invalid.')]")]
        private IWebElement InvalidLoginMessage;

        public IWebElement getInvalidLoginMessage()
        {
            return InvalidLoginMessage;
        }


        public HomePage validlogin(string emailAddress, string password)
        {
            TbEmail.SendKeys(emailAddress);

            TbPassword.SendKeys(password);

            BLogIn.Click();

            return new HomePage(driver);
        }

        public void invalidlogin(string emailAddress, string password)
        {
            TbEmail.SendKeys(emailAddress);

            TbPassword.SendKeys(password);

            BLogIn.Click();

            WaitForElementToBeEnabled(driver, InvalidLoginMessage);
        }
    }
}
