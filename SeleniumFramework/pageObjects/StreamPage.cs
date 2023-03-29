using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumFramework.utilities;

namespace SeleniumFramework.pageObjects
{
    internal class StreamPage : Base
    {
        private IWebDriver driver;
        public StreamPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Customize')]")]
        private IWebElement BCustomize;

        [FindsBy(How = How.CssSelector, Using = "c-wiz[class='SSPGKf fXYYpf'] h1[class='tNGpbb YrFhrf-ZoZQ1 YVvGBb']")]
        private IWebElement LClassName;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Select photo']")]
        private IWebElement BSelectPhoto;

        [FindsBy(How = How.XPath, Using = "//button[@id='Sports']")]
        private IWebElement TabSports;

        [FindsBy(How = How.XPath, Using = "//div[@aria-label='Theme photo options in the Sports category']/div[1]")]
        private IWebElement FirstThemInSports;

        [FindsBy(How = How.XPath, Using = "(//span[contains(text(),'Select class theme')])[2]")]
        private IWebElement BSelectClassTheme;

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Purple']")]
        private IWebElement BPurpleThemeColor;

        [FindsBy(How = How.XPath, Using = "(//span[contains(text(),'Save')])[2]")]
        private IWebElement BSaveOnCustomizeAppearance;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Class theme updated']")]
        private IWebElement MessageAfterThemeUpdate;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Post created']")]
        private IWebElement MessageAfterPost;        

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Announce something to your class')]")]
        private IWebElement LabelAnnounceSomething;

        [FindsBy(How = How.XPath, Using = "//div[@aria-label='Announce something to your class']")]
        private IWebElement TextAreaAnnouncement;

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Add YouTube video']")]
        private IWebElement BAddYouTubeVideo;

        [FindsBy(How = How.XPath, Using = "//input[@aria-label='Search']")]
        private IWebElement TbSearchYouTube;

        [FindsBy(How = How.XPath, Using = "//i[normalize-space()='search']")]
        private IWebElement BSearchVideo;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'Jz1Dmb')]//div[1]")]
        private IWebElement FirstVideoInsearchResult;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Add video']")]
        private IWebElement BAddVideo;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Post')]")]
        private IWebElement BPost;

        [FindsBy(How = How.XPath, Using = "//div[@aria-label='Save options']")]
        private IWebElement BSaveOptions;

        [FindsBy(How = How.XPath, Using = "//div[text()='Schedule']")]
        private IWebElement BSchedule;

        [FindsBy(How = How.XPath, Using = "//input[contains(@aria-label,'Scheduled date')]")]
        private IWebElement IbDate;

        [FindsBy(How = How.XPath, Using = "//input[@aria-label='Scheduled time']")]
        private IWebElement IbTime;

        [FindsBy(How = How.XPath, Using = "//button[@title='Next month']")]
        private IWebElement BNextMonth;

        [FindsBy(How = How.XPath, Using = "//table[contains(@role,'presentation')]//tbody//tr[3]//td[2]")]
        private IWebElement BSecondMonday;

        [FindsBy(How = How.XPath, Using = "(//span[text()='Schedule'])[2]")]
        private IWebElement BScheduleAnnouncement;

        [FindsBy(How = How.XPath, Using = "//div[@aria-live='polite']//div//span[contains(text(),'Scheduled')]")]
        private IWebElement MessageAfterSchedule;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Selenium Training 101']/../../../div/div[4]/div/div")]
        private IWebElement BAnnouncementOptionsForSeleniumTraining;

        [FindsBy(How = How.XPath, Using = "//div[text()='Delete']")]
        private IWebElement BDelete;

        [FindsBy(How = How.XPath, Using = "(//span[contains(text(),'Delete')])[2]")]
        private IWebElement BDeleteOnConfirmation;





        public IWebElement getMessageAfterPost()
        {
            return MessageAfterPost;
        }

        public IWebElement getMessageAfterSchedule()
        {
            return MessageAfterSchedule;
        }

        public IWebElement getMessageAfterThemeUpdate()
        {
            return MessageAfterThemeUpdate;
        }

        public IWebElement getClassNameLabel ()
        {
            return LClassName;
        }

        public void waitForClassNameDisplay()
        {
            waitForElementVisibleByCssSelector(driver, "c-wiz[class='SSPGKf fXYYpf'] h1[class='tNGpbb YrFhrf-ZoZQ1 YVvGBb']"); // Wait for Class Name on the banner
        }

        public void customizeAppearance()
        {
            BCustomize.Click();
            BSelectPhoto.Click();
            TabSports.Click();
            FirstThemInSports.Click();
            BSelectClassTheme.Click();
            BPurpleThemeColor.Click();
            BSaveOnCustomizeAppearance.Click();
            waitForElementVisibleByXpath(driver, "//span[normalize-space()='Class theme updated']");
        }

        public void scheduleAnnouncement(string postMessage)
        {
            LabelAnnounceSomething.Click();

            waitForElementClickableByXPath(driver, "//div[contains(@aria-label,'Announce something to your class')]");

            Thread.Sleep(2000); // There is delay after clicking Announce Something label.

            TextAreaAnnouncement.SendKeys(postMessage);

            Thread.Sleep(2000);

            waitForElementClickableByXPath(driver, "//div[@aria-label='Save options']");

            BSaveOptions.Click();

            waitForElementClickableByXPath(driver, "//div[text()='Schedule']");

            BSchedule.Click();

            waitForElementClickableByXPath(driver, "//input[contains(@aria-label,'Scheduled date')]");
                        
            IbDate.Click();

            BNextMonth.Click();

            BSecondMonday.Click();

            IbTime.SendKeys("10:30 AM");

            BScheduleAnnouncement.Click();

            waitForElementVisibleByXpath(driver, "//div[@aria-live='polite']//div//span[contains(text(),'Scheduled')]");
        }
        
        public void postAnnouncementWithVideo(string postMessage, string searchText)
        {
            LabelAnnounceSomething.Click();

            waitForElementClickableByXPath(driver, "//div[contains(@aria-label,'Announce something to your class')]");
            
            Thread.Sleep(2000); // There is delay after clicking Announce Something label.

            TextAreaAnnouncement.SendKeys(postMessage);

            BAddYouTubeVideo.Click();

            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("[id = 'newt-iframe']")));

            waitForElementClickableByXPath(driver, "//input[@aria-label='Search']");
            TbSearchYouTube.SendKeys(searchText);

            BSearchVideo.Click();

            waitForElementClickableByXPath(driver, "//div[contains(@class,'Jz1Dmb')]//div[1]");
            FirstVideoInsearchResult.Click();

            waitForElementClickableByXPath(driver, "//span[normalize-space()='Add video']");
            BAddVideo.Click();

            waitForElementClickableByXPath(driver, "//span[contains(text(),'Post')]");
            BPost.Click();

            waitForElementVisibleByXpath(driver, "//span[normalize-space()='Post created']");
        }

        public void deleteAnnouncement(string announcementToDelete)
        {
            waitForElementClickableByXPath(driver, $"//span[normalize-space()='{announcementToDelete}']/../../../div/div[4]/div/div");
            BAnnouncementOptionsForSeleniumTraining.Click();

            waitForElementClickableByXPath(driver, "//div[text()='Delete']");
            BDelete.Click();

            waitForElementClickableByXPath(driver, "(//span[contains(text(),'Delete')])[2]");
            BDeleteOnConfirmation.Click();

            waitForElementNotVisibleByXpath(driver, $"//span[normalize-space()='{announcementToDelete}']");
            if (driver.FindElements(By.XPath($"//span[normalize-space()='{announcementToDelete}']")).Count != 0)
            {
                // exists
                Assert.Fail("The selected announcement was not deleted successfully");
            }
            else
            {
                // doesn't exist
                Assert.Pass();
            }



        }
    }
}
