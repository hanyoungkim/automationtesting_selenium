using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFrameworkTests.Old.Pages
{
    internal class AccountCreatedPage
    {
        private IWebDriver driver;
        public AccountCreatedPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "h2[data-qa='account-created'] b")]
        private IWebElement message;

        [FindsBy(How = How.LinkText, Using = "Continue")]
        private IWebElement BContinue;

        public IWebElement getMessage()
        {
            return message;
        }

        public IWebElement getButtonContinue()
        {
            return BContinue;
        }
    }
}
