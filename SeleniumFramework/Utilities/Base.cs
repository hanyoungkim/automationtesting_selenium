using System.Configuration;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumFrameworkTests.pageObjects;
using SeleniumFrameworkTests.Utilities;
using WebDriverManager.DriverConfigs.Impl;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SeleniumFrameworkTests.utilities
{
    public class Base
    {
        public ExtentReports extent;
        public ExtentTest test;
        string browserName;

        //report file
        [OneTimeSetUp]
        public void Setup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName
                + Path.DirectorySeparatorChar + "Result"
                + Path.DirectorySeparatorChar + $"Result_{TestContext.CurrentContext.Test.Name.Trim()}_{DateTime.Now.ToString("ddMMyyyy HHmmss")}";
            string reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Tester", "Lucas");
        }

        // public IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        [SetUp]
        public void StartBrowser()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            //Configuration
            browserName = TestContext.Parameters["browserName"];

            if (browserName == null)
            {
                browserName = ConfigurationManager.AppSettings["browser"];
            }

            InitBrowser(browserName);

            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);

            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://accounts.google.com/v3/signin/identifier?dsh=S-1777991248%3A1679447350532459&continue=https%3A%2F%2Fclassroom.google.com&ifkv=AQMjQ7RnsutDbe9hfRF7XwaMokg7_Y_DW2jTlK83dcgbDM2DRKblU8YutFueLW6PLJvw_oEPpBaaCw&passive=true&flowName=GlifWebSignIn&flowEntry=ServiceLogin";
        }

        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        public void InitBrowser(string browserName)

        {
            switch (browserName)
            {
                case "Firefox":

                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;

                case "Chrome":

                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;


                case "Edge":

                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;
            }
        }

        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }

        [TearDown]
        public void AfterTest()

        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            DateTime time = DateTime.Now;
            string fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";

            if (status == TestStatus.Failed)
            {

                test.Fail("Test failed", captureScreenShot(driver.Value, fileName));
                test.Log(Status.Fail, "test failed with logtrace" + stackTrace);

            }
            else if (status == TestStatus.Passed)
            {

            }

            extent.Flush();
            driver.Value.Quit();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            
        }

        public MediaEntityModelProvider captureScreenShot(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }

        public void LoginAsTeacher()
        {
            LoginPage loginPage = new LoginPage(getDriver());
            HomePage homePage = loginPage.validlogin("lucas.selenium.classroom", "PAssword!@!@");

            homePage.waitForLucas101Display();

            Assert.That(homePage.getCreateOrJoinClassButton().Enabled, Is.True);
        }

        public void waitForElementVisibleByXpath(IWebDriver driver, string xPath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(xPath)));
        }

        public void waitForElementNotVisibleByXpath(IWebDriver driver, string xPath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.XPath(xPath)));
        }

        public void waitForElementVisibleByCssSelector(IWebDriver driver,string cssSelector)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(cssSelector)));
        }

        public void waitForElementClickableByXPath(IWebDriver driver,string xPath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(xPath)));
        }

        public void doubleClick(IWebDriver driver, IWebElement iWebElement)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(iWebElement).DoubleClick().Perform();
        }
    }
}
