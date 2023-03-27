using OpenQA.Selenium;
using SeleniumFramework.Old.Pages;
using SeleniumFramework.utilities;
using System.Collections.Immutable;

namespace SeleniumFramework.Old.Tests
{
    // [Parallelizable(ParallelScope.Children)] // run all test methods in one class parallel
    // [Parallelizable(ParallelScope.Self)] // run all test methods in one class parallel
    public class E2ETest : Base
    {
        //[Test, TestCaseSource("AddTestDataConfig"), Category("Regression")]
        //[TestCase("rahulshettyacademy", "learning")]
        //[TestCase("rahulshetty", "learning")]

        // run all data sets of Test method in parallel  - Done
        // run all test methods in one class parallel   - Done
        // run all test files in project parallel   - Done

        //dotnet test pathto.csproj ( ALl tests)
        //dotnet test pathto.csproj --filter TestCategory=Smoke

        // dotnet test SeleniumFramework.csproj --filter TestCategory=Smoke -- TestRunParameters.Parameter\(name=\"browserName\",value=\"Chrome\"\)

        // [Parallelizable(ParallelScope.All)] // run all data sets of Test method in parallel
        public void EndToEndFlow(string username, string password, string[] expectedProducts)
        {
            string[] actualProducts = new string[2];

            old_LoginPage loginPage = new old_LoginPage(getDriver());
            ProductsPage productsPage = loginPage.validLogin(username, password);

            productsPage.waitForCheckOutDisplay();

            IList<IWebElement> products = productsPage.getCards();

            foreach (IWebElement product in products)
            {
                if (expectedProducts.Contains(product.FindElement(productsPage.getCardTitle()).Text))
                {
                    product.FindElement(productsPage.addToCartButton()).Click();
                }
            }

            CheckoutPage checkoutPage = productsPage.checkout();

            IList<IWebElement> checkoutCards = checkoutPage.getCards();

            for (int i = 0; i < checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }

            Assert.That(actualProducts, Is.EqualTo(expectedProducts));

            DeliverySetupPage deliverySetupPage = checkoutPage.checkOut();

            deliverySetupPage.getTbLocation().SendKeys("ind");
            deliverySetupPage.waitForIndiaDisplay();
            deliverySetupPage.getButtonIndia().Click();
            deliverySetupPage.getCheckBoxTnC().Click();
            deliverySetupPage.purchase();

            string confirmText = deliverySetupPage.getSuccessMessage().Text;

            StringAssert.Contains("Success", confirmText);
        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username_wrong"), getDataParser().extractData("password_wrong"), getDataParser().extractDataArray("products"));
        }
    }
}
