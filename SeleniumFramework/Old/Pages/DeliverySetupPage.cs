using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.Old.Pages
{
    public class DeliverySetupPage
    {
        private IWebDriver driver;

        public DeliverySetupPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement tbLocation;

        [FindsBy(How = How.CssSelector, Using = "label[for*='checkbox2']")]
        private IWebElement checkBoxTnC;

        [FindsBy(How = How.LinkText, Using = "India")]
        private IWebElement buttonIndia;

        [FindsBy(How = How.CssSelector, Using = "[value='Purchase']")]
        private IWebElement buttonPurchase;

        [FindsBy(How = How.CssSelector, Using = ".alert-success")]
        private IWebElement successMessage;

        public IWebElement getTbLocation() { return tbLocation; }

        public IWebElement getButtonIndia() { return buttonIndia; }

        public IWebElement getCheckBoxTnC() { return checkBoxTnC; }

        public IWebElement getSuccessMessage() { return successMessage; }

        public void purchase()
        {
            buttonPurchase.Click();
        }

        public void waitForIndiaDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("India")));
        }
    }
}
