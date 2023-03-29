using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumFrameworkTests.utilities;

namespace SeleniumFrameworkTests.pageObjects
{
    internal class ClassworkPage : Base
    {
        private IWebDriver driver;
        public ClassworkPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Create']")]
        private IWebElement BCreate;

        [FindsBy(How = How.XPath, Using = "//input[@aria-label='Topic']")]
        private IWebElement IpTopic;

        [FindsBy(How = How.XPath, Using = "(//span[contains(text(),'Add')])[2]")]
        private IWebElement BAddTopic;

        [FindsBy(How = How.XPath, Using = "(//ul[@role='tablist'])[1]//li[2]//a/div[1]")]
        private IWebElement LFirstTopic;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Topic']")]
        private IWebElement BTopic;

        [FindsBy(How = How.XPath, Using = "(//li[contains(@role,'presentation')])")]
        private IList<IWebElement> Topics;

        


        public IWebElement getFirstLocatedTopic()
        {
            return LFirstTopic;
        }
        
        public void createTopic(string topicName)
        {
            waitForElementClickableByXPath(driver, "//span[normalize-space()='Create']");
            BCreate.Click();

            waitForElementClickableByXPath(driver, "//span[normalize-space()='Topic']");
            BTopic.Click();

            waitForElementClickableByXPath(driver, "//input[@aria-label='Topic']");
            IpTopic.SendKeys(topicName);
            
            BAddTopic.Click();

            Thread.Sleep(2000); // There is a delay after clicking Add button

            foreach (IWebElement Topic in Topics)
            {
                if (topicName.Contains(Topic.Text))
                {
                    Assert.Pass();
                }
            }
        }
    }
}
