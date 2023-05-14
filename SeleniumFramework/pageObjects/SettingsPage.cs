using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumFrameworkTests.utilities;

namespace SeleniumFrameworkTests.pageObjects
{
    internal class SettingsPage : Base
    {
        private IWebDriver driver;
        public SettingsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "#aria-label-settingstabs")]
        private IWebElement LSettings;

        [FindsBy(How = How.CssSelector, Using = "a[title = 'Manage folders']")]
        private IWebElement BFolders;

        [FindsBy(How = How.CssSelector, Using = "a[title = 'Create new folder'] span")]
        private IWebElement BCreateNewFolder;

        [FindsBy(How = How.CssSelector, Using = "a[title='Folder actions...'] span")]
        private IWebElement BFolderActions;

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Delete']")]
        private IWebElement BDelete;

        [FindsBy(How = How.CssSelector, Using = "button[class='mainaction delete ui-button ui-corner-all ui-widget']")]
        private IWebElement BDeleteConfirmation;

        [FindsBy(How = How.CssSelector, Using = "input[name='_name']")]
        private IWebElement IpName;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Folder successfully deleted.')]")]
        private IWebElement FolderSuccessfullyDeletedMessage;

        

        [FindsBy(How = How.CssSelector, Using = "input[name='_name']")]
        private IWebElement IpName;

        [FindsBy(How = How.CssSelector, Using = "input[name='_name']")]
        private IWebElement IpName;

        [FindsBy(How = How.CssSelector, Using = "input[name='_name']")]
        private IWebElement IpName;

    }
}
