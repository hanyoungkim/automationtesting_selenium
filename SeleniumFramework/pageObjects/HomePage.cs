using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumFrameworkTests.Tests;
using SeleniumFrameworkTests.utilities;
using System.Security.Principal;

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

        [FindsBy(How = How.XPath, Using = "//a[@class='button-addressbook']")]
        private IWebElement BContacts;

        [FindsBy(How = How.LinkText, Using = "Inbox")]
        private IWebElement BInbox;

        [FindsBy(How = How.LinkText, Using = "Compose")]
        private IWebElement BCompose;

        [FindsBy(How = How.LinkText, Using = "Drafts")]
        private IWebElement BDrafts;

        [FindsBy(How = How.XPath, Using = "(//a[text()='Reply'])[1]")]
        private IWebElement BReply;

        [FindsBy(How = How.XPath, Using = "//span[text()='Reply to sender']")]
        private IWebElement BReplyToSender;

        [FindsBy(How = How.XPath, Using = "(//a[text()='Logout'])[1]")]
        private IWebElement BLogout;

        [FindsBy(How = How.XPath, Using = "//input[@id='quicksearchbox']")]
        private IWebElement TbSearch;

        [FindsBy(How = How.XPath, Using = "//a[text()='Search']")]
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

        [FindsBy(How = How.XPath, Using = "//div[text()='Message(s) marked successfully.']")]
        private IWebElement SuccessfullyMarkedMessage;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'messages found')]")]
        private IWebElement MessagesFound;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'moved to Trash successfully.')]")]
        private IWebElement MessagesMovedToTrashMessage;

        [FindsBy(How = How.XPath, Using = "//select[@id='messagessearchfilter']")]
        private SelectElement SearchFilter;

        [FindsBy(How = How.XPath, Using = "//a[@id='listselectmenulink']")]
        private IWebElement BSelectMenu;

        [FindsBy(How = How.LinkText, Using = "Delete")]
        private IWebElement BDelete;

        [FindsBy(How = How.LinkText, Using = "Sent")]
        private IWebElement BSent;

        [FindsBy(How = How.XPath, Using = "//span[@class='icon mail']")]
        private IWebElement BAll;

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

            WaitForElementToBeEnabled(driver, MessagesFound);

            bool testPassed = false;

            foreach (IWebElement message in Messages)
            {
                if (message.Text.Contains(subjectToSearch))
                {
                    testPassed = true;
                    break;
                }
            }

            Assert.IsTrue(testPassed);
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

                    Wait(2000);

                    message.FindElement(ByFlag).Click();
                }
            }

            WaitForElementToBeEnabled(driver, SuccessfullyMarkedMessage);

            SearchFilter.SelectByValue("FLAGGED");

            WaitForElementToBeEnabled(driver, MessagesFound);

            Assert.IsTrue(Messages.Any(message => message.Text.Contains(emailToFlag)));
        }

        public void DeleteAllEmailsFromInbox()
        {
            BSelectMenu.Click();

            WaitForElementToBeEnabled(driver, BAll);

            BAll.Click();

            Wait(1000);

            WaitForElementToBeEnabled(driver, BDelete);

            BDelete.Click();

            WaitForElementToBeEnabled(driver, MessagesMovedToTrashMessage);
        }

        public void goToSentFolder()
        {
            BSent.Click();

            WaitForElementToBeEnabled(driver, FirstMessage);
        }

        public void DeleteAllEmailsFromSent()
        {
            BSelectMenu.Click();

            WaitForElementToBeEnabled(driver, BAll);

            BAll.Click();

            Wait(1000);

            WaitForElementToBeEnabled(driver, BDelete);

            BDelete.Click();

            WaitForElementToBeEnabled(driver, MessagesMovedToTrashMessage);
        }
    }
}
