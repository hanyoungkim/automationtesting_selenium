using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumFramework.Old.Pages
{
    public class old_LoginPage
    {
        private IWebDriver driver;
        public old_LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        // Pageobject factory
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement tbUsername;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement tbPassword;

        [FindsBy(How = How.XPath, Using = "//div[@class='form-group'][5]/label/span/input")]
        private IWebElement checkBox;

        [FindsBy(How = How.XPath, Using = "//input[@value='Sign In']")]
        private IWebElement signInButton;

        public ProductsPage validLogin(string username, string password)
        {
            tbUsername.SendKeys(username);
            tbPassword.SendKeys(password);
            checkBox.Click();
            signInButton.Click();

            return new ProductsPage(driver);
        }

        public IWebElement getUserName() { return tbUsername; }
    }
}
