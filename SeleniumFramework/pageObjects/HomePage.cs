using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumFramework.utilities;

namespace SeleniumFramework.pageObjects
{
    internal class HomePage : Base
    {
        private IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "button[aria-label='Create or join a class']")]
        private IWebElement BCreateOrJoinClass;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Create class']")]
        private IWebElement BCreateClass;

        [FindsBy(How = How.XPath, Using = "//input[@type='checkbox']")]
        private IWebElement CbReadAndUnderstand;

        [FindsBy(How = How.CssSelector, Using = "div[class='uArJ5e UQuaGc kCyAyd l3F1ye ARrCac HvOprf evJWRb M9Bg4d'] span[class='NPEfkd RveJvd snByac']")]
        private IWebElement BContinue;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Class name (required)']//following-sibling::input[1]")]
        private IWebElement TbClassName;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Section']//following-sibling::input[1]")]
        private IWebElement TbSection;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Subject']//following-sibling::input[1]")]
        private IWebElement TbSubject;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Room']//following-sibling::input[1]")]
        private IWebElement TbRoom;

        [FindsBy(How = How.XPath, Using = "(//div[@aria-label='Create'])[2]//following-sibling::span[1]//span")]
        private IWebElement BCreate;

        [FindsBy(How = How.XPath, Using = "(//span[contains(text(),'Archive')])[1]")]
        private IWebElement BArchive;

        [FindsBy(How = How.XPath, Using = "(//button[contains(@aria-label,'Class options')])[1]")]
        private IWebElement BMoreFor1stClassroom;

        [FindsBy(How = How.XPath, Using = "(//span[contains(text(),'Archive')])[4]")]
        private IWebElement BArchiveOnConfirmationPopup;

        [FindsBy(How = How.XPath, Using = "//a[@target='_self']//div[contains(text(),'Class 101')]")]
        private IWebElement BTestClassroom;


        public StreamPage goToTestClassroom()
        {
            BTestClassroom.Click();
            return new StreamPage(driver);
        }

        public IWebElement getCreateOrJoinClassButton()
        {
            return BCreateOrJoinClass;
        }

        public void waitForLucas101Display()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[@target='_self']//div[contains(text(),'Lucas 101')]")));
        }

        public StreamPage createNewClassroom(string className, string section, string subject, string room)
        {
            BCreateOrJoinClass.Click();
            BCreateClass.Click();
            CbReadAndUnderstand.Click();
            BContinue.Click();

            waitForElementClickableByXPath(driver, "//span[normalize-space()='Class name (required)']//following-sibling::input[1]");
            TbClassName.SendKeys(className);

            waitForElementClickableByXPath(driver, "//span[normalize-space()='Section']//following-sibling::input[1]");
            TbSection.SendKeys(section);

            waitForElementClickableByXPath(driver, "//span[normalize-space()='Subject']//following-sibling::input[1]");
            TbSubject.SendKeys(subject);

            waitForElementClickableByXPath(driver, "//span[normalize-space()='Room']//following-sibling::input[1]");
            TbRoom.SendKeys(room);

            waitForElementClickableByXPath(driver, "//span[normalize-space()='Class name (required)']//following-sibling::input[1]");
            BCreate.Click();

            return new StreamPage(driver);
        }

        public void deleteClassroom()
        {
            BMoreFor1stClassroom.Click();
            
            BArchive.Click();

            waitForElementClickableByXPath(driver, "(//span[contains(text(),'Archive')])[4]");
            BArchiveOnConfirmationPopup.Click();

            if (driver.FindElements(By.XPath("//a[@target='_self']//div[contains(text(),'Class 101')]")).Count != 0)
            {
                // exists
                Assert.Fail("The selected classroom was not deleted successfully");
            }
            else
            {
                // doesn't exist
                Assert.Pass();
            }
        }
    }
}
