using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.Old.Pages
{
    internal class DeleteAccountPage
    {
        private IWebDriver driver;
        public DeleteAccountPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "h2[data-qa='account-deleted']")]
        private IWebElement message;

        public IWebElement getMessage()
        {
            return message;
        }
    }
}
