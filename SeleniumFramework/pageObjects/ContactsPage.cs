using OpenQA.Selenium;
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

        [FindsBy(How = How.XPath, Using = "//span[text()='Add group']")]
        private IWebElement BAddGroup;

        [FindsBy(How = How.XPath, Using = "//label[text()='Name']//input[@type='text']")]
        private IWebElement TbGroupName;

        [FindsBy(How = How.XPath, Using = "//button[text()='Save']")]
        private IWebElement BSave;

        [FindsBy(How = How.XPath, Using = "//span[text()='Add contact']")]
        private IWebElement BAddContact;

        [FindsBy(How = How.XPath, Using = "//iframe[@id='contact-frame']")]
        private IWebElement IFrameContactFrame;

        [FindsBy(How = How.XPath, Using = "//input[@id='upload-formInput']")]
        private IWebElement IpPhotoImageSource;

        [FindsBy(How = How.XPath, Using = "//input[@id='ff_firstname']")]
        private IWebElement TbFirstName;

        [FindsBy(How = How.XPath, Using = "//input[@id='ff_surname']")]
        private IWebElement TbLastName;

        [FindsBy(How = How.XPath, Using = "//input[@id='ff_email0']")]
        private IWebElement TbEmail;

        [FindsBy(How = How.XPath, Using = "//input[@id='ff_phone0']")]
        private IWebElement TbPhone;

        [FindsBy(How = How.XPath, Using = "//input[@value='Save']")]
        private IWebElement BSaveContact;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Successfully saved')]")]
        private IWebElement SuccessfullySavedMessage;

        [FindsBy(How = How.XPath, Using = "//ul[@role='group']//li[1]")]
        private IWebElement BFirstGroup;

        [FindsBy(How = How.XPath, Using = "//a[text()='Groups']")]
        private IWebElement BGroups;

        [FindsBy(How = How.XPath, Using = "//label[text()='Test Group 01']")]
        private IWebElement CbTestGroup01;

        [FindsBy(How = How.XPath, Using = "//a[text()='Test Group 01']")]
        private IWebElement LTestGroup01;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Group created successfully')]")]
        private IWebElement GroupCreatedMessage;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Contact(s) deleted successfully')]")]
        private IWebElement ContactDeletedMessage;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Group deleted successfully')]")]
        private IWebElement GroupDeletedMessage;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Loading...')]")]
        private IWebElement LoadingMessage;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Successfully added the contacts to this group')]")]
        private IWebElement SuccessfullyAddedContactsToGroupMessage;

        [FindsBy(How = How.XPath, Using = "//a[@title='Delete selected contacts']")]
        private IWebElement ADeleteSelectedContact;

        [FindsBy(How = How.XPath, Using = "//span[text()='Delete selected contacts']")]
        private IWebElement BDeleteSelectedContact;

        [FindsBy(How = How.XPath, Using = "//button[text()='Delete']")]
        private IWebElement BDelete;

        [FindsBy(How = How.XPath, Using = "//span[text()='Addressbook/group options']")]
        private IWebElement BGroupOptions;

        [FindsBy(How = How.XPath, Using = "//a[text()='Delete group']")]
        private IWebElement BDeleteGroup;

        




        [FindsBy(How = How.XPath, Using = "//table[@role='listbox']//tr")]
        private IList<IWebElement> Contacts;


        public void createNewGroup()
        {
            WaitForElementToBeEnabled(driver, BAddGroup);

            Wait(1000);

            BAddGroup.Click();

            WaitForElementToBeEnabled(driver, TbGroupName);

            TbGroupName.SendKeys("Test Group 01");

            BSave.Click();

            WaitForElementToBeEnabled(driver, GroupCreatedMessage);
        }

        public void addNewContact(string firstName, string lastName, string email, string phone)
        {
            BAddContact.Click();

            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(IFrameContactFrame);

            string photoImageFilePath = Path.Combine(getProjectDirectory(), "TestFiles\\testFace.png");

            IpPhotoImageSource.SendKeys(photoImageFilePath);

            WaitForElementToBeEnabled(driver, TbFirstName);

            TbFirstName.SendKeys(firstName);
            TbLastName.SendKeys(lastName);
            TbEmail.SendKeys(email);
            TbPhone.SendKeys(phone);

            BSaveContact.Click();

            driver.SwitchTo().ParentFrame();

            WaitForElementToBeEnabled(driver, SuccessfullySavedMessage);

            StringAssert.Contains("Successfully saved", SuccessfullySavedMessage.Text);
        }

        public void addContactToGroup()
        {
            foreach (IWebElement contact in Contacts)
            {
                if (contact.Text.Contains("Lucas Kim"))
                {
                    contact.Click();
                }
            }

            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(IFrameContactFrame);

            WaitForElementToBeEnabled(driver, BGroups);
            BGroups.Click();

            WaitForElementToBeEnabled(driver, CbTestGroup01);
            CbTestGroup01.Click();

            driver.SwitchTo().ParentFrame();

            WaitForElementToBeEnabled(driver, SuccessfullyAddedContactsToGroupMessage);

            BFirstGroup.Click();

            Wait(1000);

            // --> Use Lamda Expression.
            // This code uses the Any method from LINQ to determine whether any element in the Contacts list satisfies the given condition.
            // If at least one contact's text contains the string "Lucas Kim", testPassed will be set to true.
            // Then the Assert.IsTrue method is called to assert that testPassed is true, indicating that the test has passed.
            bool testPassed = Contacts.Any(contact => contact.Text.Contains("Lucas Kim"));
            Assert.IsTrue(testPassed);
            // Assert.IsTrue(Contacts.Any(contact => contact.Text.Contains("Lucas Kim")));

            // --> Use if statement
            //bool testPassed = false;
            //foreach (IWebElement contact in Contacts)
            //{
            //    if (contact.Text.Contains("Lucas Kim"))
            //    {
            //        testPassed = true;
            //        break;
            //    }
            //}

            //Assert.IsTrue(testPassed);
        }

        public void deleteContact()
        {
            foreach (IWebElement contact in Contacts)
            {
                if (contact.Text.Contains("Lucas Kim"))
                {
                    contact.Click();
                }
            }

            WaitUntilAttributeChanges(driver, ADeleteSelectedContact, "aria-disabled", "false");

            Wait(1000);

            BDeleteSelectedContact.Click();

            WaitForElementToBeEnabled(driver, BDelete);

            BDelete.Click();

            WaitForElementToBeEnabled(driver, ContactDeletedMessage);

            LTestGroup01.Click();

            BGroupOptions.Click();

            WaitUntilAttributeChanges(driver, BDeleteGroup, "aria-disabled", "false");

            BDeleteGroup.Click();

            WaitForElementToBeEnabled(driver, BDelete);

            BDelete.Click();

            WaitForElementToBeEnabled(driver, ContactDeletedMessage);

            StringAssert.Contains("Group deleted successfully", GroupDeletedMessage.Text);
        }
    }
}