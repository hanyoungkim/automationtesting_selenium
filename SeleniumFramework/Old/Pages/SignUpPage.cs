using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace SeleniumFrameworkTests.Old.Pages
{
    internal class SignUpPage
    {
        private IWebDriver driver;
        public SignUpPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='id_gender1']")]
        private IWebElement RbMr;

        [FindsBy(How = How.XPath, Using = "//input[@id='id_gender2']")]
        private IWebElement RbMrs;

        [FindsBy(How = How.CssSelector, Using = "#name")]
        private IWebElement TbName;

        [FindsBy(How = How.CssSelector, Using = "#email")]
        private IWebElement TbEmail;

        [FindsBy(How = How.CssSelector, Using = "#password")]
        private IWebElement TbPassword;

        [FindsBy(How = How.CssSelector, Using = "select[data-qa='days']")]
        private SelectElement DdDay;

        [FindsBy(How = How.CssSelector, Using = "select[data-qa='months']")]
        private SelectElement DdMonth;

        [FindsBy(How = How.CssSelector, Using = "select[data-qa='years']")]
        private SelectElement DdYear;

        [FindsBy(How = How.CssSelector, Using = "#first_name")]
        private IWebElement TbFirstName;

        [FindsBy(How = How.CssSelector, Using = "#last_name")]
        private IWebElement TbLastName;

        [FindsBy(How = How.CssSelector, Using = "#address1")]
        private IWebElement TbAddress1;

        [FindsBy(How = How.CssSelector, Using = "select[data-qa='country']")]
        private SelectElement DdCountry;

        [FindsBy(How = How.CssSelector, Using = "#state")]
        private IWebElement TbState;

        [FindsBy(How = How.CssSelector, Using = "#city")]
        private IWebElement TbCity;

        [FindsBy(How = How.CssSelector, Using = "#zipcode")]
        private IWebElement TbZipcode;

        [FindsBy(How = How.CssSelector, Using = "#mobile_number")]
        private IWebElement TbMobileNumber;

        [FindsBy(How = How.CssSelector, Using = "button[data-qa='create-account']")]
        private IWebElement BCreateAccount;

        public AccountCreatedPage completeSignUp(
            string Title, string password, string day, string month, string year, string firstName,
            string lastName, string address1, string country, string state, string city, string zipcode, string mobileNumber)
        {
            if (Title == "Mr")
            {
                RbMr.Click();
            }
            else if (Title == "Mrs")
            {
                RbMr.Click();
            }

            TbPassword.SendKeys(password);

            DdDay.SelectByValue(day);
            DdMonth.SelectByText(month);
            DdYear.SelectByValue(year);

            TbFirstName.SendKeys(firstName);
            TbLastName.SendKeys(lastName);

            TbAddress1.SendKeys(address1);
            DdCountry.SelectByValue(country);
            TbState.SendKeys(state);
            TbCity.SendKeys(city);
            TbZipcode.SendKeys(zipcode);

            TbMobileNumber.SendKeys(mobileNumber);

            IJavaScriptExecutor javascriptExecutor = (IJavaScriptExecutor)driver;
            javascriptExecutor.ExecuteScript("arguments[0].click();", BCreateAccount);

            return new AccountCreatedPage(driver);
        }
    }
}
