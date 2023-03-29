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

        [FindsBy(How = How.XPath, Using = "//input[@id='identifierId']")]
        private IWebElement TbEmail;

        [FindsBy(How = How.CssSelector, Using = "div[id='identifierNext'] span")]
        private IWebElement BNextOnEmail;

        [FindsBy(How = How.CssSelector, Using = "div[id='passwordNext'] span")]
        private IWebElement BNextOnPassword;

        [FindsBy(How = How.CssSelector, Using = "input[name = 'Passwd']")]
        private IWebElement TbPassword;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Wrong password. Try again or click Forgot password')]")]
        private IWebElement LWrongPassword;

        public IWebElement getWrongPasswordMessage()
        {
            return LWrongPassword;
        }
        

        public HomePage validlogin(string emailAddress, string password)
        {
            TbEmail.SendKeys(emailAddress);
            
            BNextOnEmail.Click();

            TbPassword.SendKeys(password);

            BNextOnPassword.Click();

            return new HomePage(driver);
        }

        public void invalidlogin(string emailAddress, string password)
        {
            TbEmail.SendKeys(emailAddress);

            BNextOnEmail.Click();

            TbPassword.SendKeys(password);

            BNextOnPassword.Click();

            waitForElementVisibleByXpath(driver, "//span[contains(text(),'Wrong password. Try again or click Forgot password')]");
        }

        public void waitForNextButtonDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div[id='passwordNext'] span")));
        }
    }
}
