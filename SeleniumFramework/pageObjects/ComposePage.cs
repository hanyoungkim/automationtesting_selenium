using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumFrameworkTests.utilities;

namespace SeleniumFrameworkTests.pageObjects
{
    internal class ComposePage : Base
    {
        private IWebDriver driver;
        public ComposePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);            
        }

        [FindsBy(How = How.XPath, Using = "//textarea[@id='_to']")]
        private IWebElement TaTo;

        [FindsBy(How = How.XPath, Using = "//input[@id='compose-subject']")]
        private IWebElement ISubject;

        [FindsBy(How = How.XPath, Using = "//textarea[@id='composebody']")]
        private IWebElement TaMessage;

        [FindsBy(How = How.CssSelector, Using = "body p")]
        private IWebElement HtmlMessageArea;

        [FindsBy(How = How.LinkText, Using = "Send")]
        private IWebElement BSend;

        [FindsBy(How = How.XPath, Using = "//div[normalize-space()='Message sent successfully.']")]
        private IWebElement SuccessfullySentMessage;

        [FindsBy(How = How.XPath, Using = "//div[normalize-space()='Message saved to Drafts.']")]
        private IWebElement SuccessfullySavedToDraftsMessage;

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Add Cc']")]
        private IWebElement BAddCc;

        [FindsBy(How = How.XPath, Using = "//textarea[@name='_cc']")]
        private IWebElement TaCc;

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Add Bcc']")]
        private IWebElement BAddBcc;

        [FindsBy(How = How.XPath, Using = "//textarea[@name='_bcc']")]
        private IWebElement TaBcc;

        [FindsBy(How = How.XPath, Using = "//input[@type='file']")]
        private IWebElement IpAttachment;

        [FindsBy(How = How.XPath, Using = "(//span[@class='attachment-name'])")]
        private IList<IWebElement> Attachments;

        [FindsBy(How = How.XPath, Using = "//select[@name='editorSelector']")]
        private SelectElement EditorType;

        [FindsBy(How = How.XPath, Using = "//select[@id='rcmcomposepriority']")]
        private SelectElement Priority;

        [FindsBy(How = How.XPath, Using = "//div[@aria-label='Insert/edit image']")]
        private IWebElement BInsertImage;

        [FindsBy(How = How.XPath, Using = "//label[normalize-space()='Source']//following-sibling::div//input")]
        private IWebElement IpImageSource;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Ok']")]
        private IWebElement BOk;

        [FindsBy(How = How.XPath, Using = "//iframe[@id='composebody_ifr']")]
        private IWebElement IFrameHtmlMessageArea;

        [FindsBy(How = How.CssSelector, Using = "a[title='Save as draft']")]
        private IWebElement BSave;

        private By BySuccessfullySentMessage = By.XPath("//div[normalize-space()='Message sent successfully.']");

        private By BySuccessfullySavedToDraftsMessage = By.XPath("//div[normalize-space()='Message saved to Drafts.']");

        public void saveMessage(string toAddress, string subject)
        {
            WaitForElementToBeClickable(driver, TaTo);

            TaTo.SendKeys(toAddress);
            ISubject.SendKeys(subject);
            TaMessage.SendKeys("This is a saved message by automation test." + " Today is - " + DateTime.Today.ToString());

            BSave.Click();

            WaitForElementToBeVisible(driver, BySuccessfullySavedToDraftsMessage);

            StringAssert.Contains("Message saved to Drafts", SuccessfullySavedToDraftsMessage.Text);
        }

        public void sendPlainEmail(string toAddress, string subject, string message)
        {
            WaitForElementToBeClickable(driver, TaTo);

            TaTo.SendKeys(toAddress);
            ISubject.SendKeys(subject);
            TaMessage.SendKeys(message + " Today is - " + DateTime.Today.ToString());

            sendEmail();
        }

        public void sendEmailWithCCAndBcc(string toAddress, string ccEmail, string bcc, string subject, string message)
        {
            WaitForElementToBeClickable(driver, TaTo);

            TaTo.SendKeys(toAddress);

            BAddCc.Click();

            WaitForElementToBeClickable(driver, TaCc);

            TaCc.SendKeys(ccEmail);

            BAddBcc.Click();

            WaitForElementToBeClickable(driver, TaBcc);

            TaBcc.SendKeys(bcc);

            ISubject.SendKeys(subject);
            TaMessage.SendKeys(message + " Today is - " + DateTime.Today.ToString());

            sendEmail();
        }

        public void sendEmailWith2Attachments(string toAddress, string subject, string message, string fileName1, string fileName2)
        {
            WaitForElementToBeClickable(driver, TaTo);

            TaTo.SendKeys(toAddress);
            ISubject.SendKeys(subject);
            TaMessage.SendKeys(message + " Today is - " + DateTime.Today.ToString());

            // ProjectDirectory --> TestFiles Folder --> Test files (.txt)
            string testFile1Path = Path.Combine(getProjectDirectory(), "TestFiles\\test.txt");
            string testFile2Path = Path.Combine(getProjectDirectory(), "TestFiles\\test2.txt");

            IpAttachment.SendKeys(testFile1Path);

            try
            {
                foreach (IWebElement attachment in Attachments)
                {
                    if (attachment.Text.Contains(fileName1))
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

            Wait(2000);

            IpAttachment.SendKeys(testFile2Path);

            try
            {
                foreach (IWebElement attachment in Attachments)
                {
                    if (attachment.Text.Contains(fileName2))
                    {
                        Assert.Pass();
                    }
                }
            }
            catch (SuccessException)
            {
                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Wait(2000);

            sendEmail();
        }

        public void sendEmailWithImage(string toAddress, string subject)
        {
            WaitForElementToBeClickable(driver, TaTo);

            TaTo.SendKeys(toAddress);
            ISubject.SendKeys(subject);

            EditorType.SelectByValue("html");
            Priority.SelectByText("Highest");

            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(IFrameHtmlMessageArea);

            HtmlMessageArea.SendKeys("The below is a test image added by automation test." + Keys.Enter);

            driver.SwitchTo().ParentFrame();

            BInsertImage.Click();

            WaitForElementToBeClickable(driver, IpImageSource);
            IpImageSource.SendKeys("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRQw_Md958pXLavljQBQnPQXrcr7nblZjcV0kx6vc29&s");

            BOk.Click();

            sendEmail();
        }

        public void editDraftedMessageAndSend()
        {
            WaitForElementToBeClickable(driver,TaMessage);

            TaMessage.Clear();
            TaMessage.SendKeys("This is a Edited message by automation test." + " Today is -" + DateTime.Today.ToString());

            sendEmail();
        }

        public void replyEmail(string message)
        {
            WaitForElementToBeClickable(driver, TaMessage);
            TaMessage.SendKeys(Keys.Enter +  message);

            sendEmail();
        }

        public void sendEmail()
        {
            BSend.Click();

            WaitForElementToBeVisible(driver, BySuccessfullySentMessage);

            StringAssert.Contains("Message sent successfully", SuccessfullySentMessage.Text);
        }
    }
}
