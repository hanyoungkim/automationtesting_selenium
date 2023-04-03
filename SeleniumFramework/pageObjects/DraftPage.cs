using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumFrameworkTests.utilities;

namespace SeleniumFrameworkTests.pageObjects
{
    internal class DraftPage : Base
    {
        private IWebDriver driver;
        public DraftPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "(//td[@class='subject'])")]
        private IList<IWebElement> Messages;

        [FindsBy(How = How.XPath, Using = "//iframe[@id='messagecontframe']")]
        private IWebElement IFrameMessageContFrame;

        [FindsBy(How = How.LinkText, Using = "Edit")]
        private IWebElement BEdit;

        public ComposePage sendDraftedMessage(string subject)
        {
            foreach (IWebElement message in Messages)
            {
                if (message.Text.Contains(subject))
                {
                    message.Click();
                }
            }

            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(IFrameMessageContFrame);

            BEdit.Click();

            return new ComposePage(driver);
        }
    }
}
