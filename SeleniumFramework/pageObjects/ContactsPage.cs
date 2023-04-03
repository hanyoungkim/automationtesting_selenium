using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumFrameworkTests.utilities;

namespace SeleniumFrameworkTests.pageObjects
{
    internal class ContactsPage : Base
    {
        private IWebDriver driver;
        public ContactsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "(//td[@class='subject'])")]
        private IList<IWebElement> Messages;

        
    }
}
