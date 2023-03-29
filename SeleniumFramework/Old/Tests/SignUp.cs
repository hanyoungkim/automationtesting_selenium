using OpenQA.Selenium;
using SeleniumFrameworkTests.pageObjects;
using SeleniumFrameworkTests.utilities;
using System.Collections.Immutable;

namespace SeleniumFrameworkTests.Old.Tests
{
    [Parallelizable(ParallelScope.Children)]
    public class SignUp : Base
    {
        //
        // Sign up  --> Delete account
        //

        //[Test, TestCaseSource("AddTestDataConfig")]
        //[Category("Regression")]
        //public void ValidSignUp(
        //    string title, string name, string password, string day, string month, string year,
        //    string firstName, string lastName, string address1, string country, string state, string city, string zipcode, string mobileNumber)
        //{
        //    HomePage homePage = new HomePage(getDriver());

        //    LoginPage loginPage = homePage.goToSignupLoginPage();

        //    SignUpPage signUpPage = loginPage.startSignUp(name);

        //    AccountCreatedPage accountCreatedPage = signUpPage.completeSignUp(title, password, day, month, year, firstName, lastName, address1, country, state, city, zipcode, mobileNumber);

        //    StringAssert.Contains("ACCOUNT CREATED", accountCreatedPage.getMessage().Text);

        //    accountCreatedPage.getButtonContinue().Click();

        //    StringAssert.Contains("FEATURES ITEMS", homePage.getLableFeaturesItems().Text);

        //    DeleteAccountPage deleteAccountPage = homePage.deleteCurrentAccount();

        //    StringAssert.Contains("ACCOUNT DELETED",deleteAccountPage.getMessage().Text);
        //}

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(
                getDataParser().extractData("title"),
                getDataParser().extractData("name"),
                getDataParser().extractData("password"),
                getDataParser().extractData("day"),
                getDataParser().extractData("month"),
                getDataParser().extractData("year"),
                getDataParser().extractData("firstName"),
                getDataParser().extractData("lastName"),
                getDataParser().extractData("address1"),
                getDataParser().extractData("country"),
                getDataParser().extractData("state"),
                getDataParser().extractData("city"),
                getDataParser().extractData("zipcode"),
                getDataParser().extractData("mobileNumber"));
        }
    }
}
