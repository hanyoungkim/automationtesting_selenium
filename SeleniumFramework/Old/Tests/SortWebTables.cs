using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using System.Collections;

namespace SeleniumFrameworkTests.Old.Tests
{
    // [Parallelizable(ParallelScope.Self)]
    public class SortWebTables
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
        }

        //[Test]
        public void SortTable()
        {
            ArrayList a = new ArrayList();

            SelectElement dropdown = new SelectElement(driver.FindElement(By.Id("page-menu")));
            dropdown.SelectByValue("20");

            // Step 1 - Get all veggie names into arraylist A
            IList<IWebElement> veggies = driver.FindElements(By.XPath("//tr/td[1]"));

            foreach (IWebElement veggie in veggies)
            {
                a.Add(veggie.Text);
            }

            // Step 2 - sort this arraylist A
            foreach (string element in a)
            {
                TestContext.Progress.WriteLine(element);
            }

            TestContext.Progress.WriteLine("After sorting ==>");
            a.Sort();

            foreach (string element in a)
            {
                TestContext.Progress.WriteLine(element);
            }

            // Step 3 - go and click column to sort
            driver.FindElement(By.CssSelector("th[aria-label *='fruit name']")).Click();

            // Step 4 - Get all veggie name into arrayList B
            ArrayList b = new ArrayList();

            IList<IWebElement> sortedVeggies = driver.FindElements(By.XPath("//tr/td[1]"));

            foreach (IWebElement veggie in sortedVeggies)
            {
                b.Add(veggie.Text);
            }

            // Step 5 - compare A and B arraylist
            Assert.That(a, Is.EqualTo(b));
        }

        [TearDown]
        public void AfterTest()
        {
            driver.Quit();
        }
    }
}
