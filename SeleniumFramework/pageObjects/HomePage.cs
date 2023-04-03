using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumFrameworkTests.utilities;

namespace SeleniumFrameworkTests.pageObjects
{
    internal class HomePage : Base
    {
        private IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//span[@class='tooltip' and text()='Mail']")]
        private IWebElement BMail;

        [FindsBy(How = How.XPath, Using = "//span[@class='tooltip' and text()='Contacts']")]
        private IWebElement BContacts;

        [FindsBy(How = How.LinkText, Using = "Inbox")]
        private IWebElement BInbox;

        [FindsBy(How = How.LinkText, Using = "Compose")]
        private IWebElement BCompose;

        [FindsBy(How = How.LinkText, Using = "Drafts")]
        private IWebElement BDrafts;

        [FindsBy(How = How.XPath, Using = "(//a[normalize-space()='Reply'])[1]")]
        private IWebElement BReply;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Reply to sender']")]
        private IWebElement BReplyToSender;

        [FindsBy(How = How.XPath, Using = "(//a[normalize-space()='Logout'])[1]")]
        private IWebElement BLogout;

        [FindsBy(How = How.XPath, Using = "//input[@id='quicksearchbox']")]
        private IWebElement TbSearch;

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Search']")]
        private IWebElement BSearch;

        [FindsBy(How = How.XPath, Using = "(//td[@class='subject'])")]
        private IList<IWebElement> Messages;

        [FindsBy(How = How.XPath, Using = "(//td[@class='subject'])[1]")]
        private IWebElement FirstMessage;

        [FindsBy(How = How.XPath, Using = "//a[@id='searchmenulink']")]
        private IWebElement BSearchOption;

        [FindsBy(How = How.XPath, Using = "//iframe[@id='messagecontframe']")]
        private IWebElement IFrameMessageContFrame;

        [FindsBy(How = How.XPath, Using = "(//td[@class='flag'])//span")]
        private IWebElement BFlag;

        [FindsBy(How = How.XPath, Using = "//div[normalize-space()='Message(s) marked successfully.']")]
        private IWebElement SuccessfullyMarkedsMessage;

        [FindsBy(How = How.XPath, Using = "//select[@id='messagessearchfilter']")]
        private SelectElement SearchFilter;

        private By BySuccessfullyMarkedMessage = By.XPath("//div[normalize-space()='Message(s) marked successfully.']");

        private By ByMessagesFound = By.XPath("//div[contains(text(),'messages found')]");

        private By ByFlag = By.XPath("following-sibling::td[@class='flag']");

        public IWebElement getFirstMessage()
        {
            return FirstMessage;
        }

        public IWebElement getLogoutButton()
        {
            return BLogout;
        }

        public ComposePage goToComposePage()
        {
            BCompose.Click();
            return new ComposePage(driver);
        }

        public DraftPage goToDraftsPage()
        {
            BDrafts.Click();
            return new DraftPage(driver);
        }

        public ContactsPage goToContactsPage()
        {
            BContacts.Click();
            return new ContactsPage(driver);
        }

        public void searchEmail(string subjectToSearch)
        {
            TbSearch.SendKeys(subjectToSearch);
            BSearchOption.Click();
            BSearch.Click();

            try
            {
                foreach (IWebElement message in Messages)
                {
                    if (message.Text.Contains(subjectToSearch))
                    { 
                        Assert.Pass();
                    }
                }
            }
            catch (SuccessException)
            {
                // The test passed, do nothing.
            }
            catch (Exception ex)
            {
                // Handle any other exception that may occur during the test.
                Assert.Fail(ex.Message);
            }
        }

        public ComposePage selectEmailToReply(string emailToReply)
        {
            foreach (IWebElement message in Messages)
            {
                if (message.Text.Contains(emailToReply))
                {
                    message.Click();
                }
            }

            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(IFrameMessageContFrame);

            BReplyToSender.Click();

            return new ComposePage(driver);
        }

        public void flagEmail(string emailToFlag)
        {
            foreach (IWebElement message in Messages)
            {
                if (message.Text.Contains(emailToFlag))
                {
                    message.Click();
                    message.FindElement(ByFlag).Click();
                }
            }

            WaitForElementToBeVisible(driver, BySuccessfullyMarkedMessage);

            SearchFilter.SelectByValue("FLAGGED");

            WaitForElementToBeVisible(driver, ByMessagesFound);

            try
            {
                foreach (IWebElement message in Messages)
                {
                    if (message.Text.Contains(emailToFlag))
                    {
                        Assert.Pass();
                    }
                }
            }
            catch (SuccessException)
            {
                // The test passed, do nothing.
            }
            catch (Exception ex)
            {
                // Handle any other exception that may occur during the test.
                Assert.Fail(ex.Message);
            }
        }
    }
}
