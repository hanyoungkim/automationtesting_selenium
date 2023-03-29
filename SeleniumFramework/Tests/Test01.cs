using SeleniumFrameworkTests.pageObjects;
using SeleniumFrameworkTests.utilities;

namespace SeleniumFrameworkTests.Tests
{
    [Order(4)]
    public class Test01 : Base
    {
        [Order(1)]
        public void Test01Test()
        {
            getDriver();

            driver.Value.Navigate().GoToUrl("https://google.com/");

            Assert.Pass();
        }
    }
}
