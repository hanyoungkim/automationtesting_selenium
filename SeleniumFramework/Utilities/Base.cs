using System.Configuration;
using System.Text.RegularExpressions;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V108.Page;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumFrameworkTests.pageObjects;
using SeleniumFrameworkTests.Utilities;
using WebDriverManager.DriverConfigs.Impl;


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
            driver.Value.Url = "https://box5877.bluehost.com:2096/";
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

                // Save the screenshot file
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName
                    + Path.DirectorySeparatorChar + "Result"
                    + Path.DirectorySeparatorChar + "Screenshots For Failed Tests"
                    + Path.DirectorySeparatorChar + $"SS_{TestContext.CurrentContext.Test.MethodName.Trim()}_{DateTime.Now.ToString("ddMMyyyy HHmmss")}";

                //string pattern = @"\(\""[^\""]+\"",""[^\""]+\""\)";
                //string replacement = "";
                //string cleanedPath = Regex.Replace(projectDirectory, pattern, replacement).Replace(" ", "_");

                Directory.CreateDirectory(projectDirectory);

                string screenshotPath = Path.Combine(projectDirectory, fileName);

                Screenshot screenshot = captureScreenShot(driver.Value);
                screenshot.SaveAsFile(screenshotPath);

                TestContext.AddTestAttachment(screenshotPath);
            }
            else if (status == TestStatus.Passed)
            {
                // Do nothing
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

        public Screenshot captureScreenShot(IWebDriver driver)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            return ts.GetScreenshot();
        }

        public void LoginAsAdminUser()
        {
            LoginPage loginPage = new LoginPage(getDriver());
            HomePage homePage = loginPage.validlogin("admin@sksolution.co.nz", "PAssword!@!@");

            WaitForElementToBeEnabled(getDriver(), homePage.getLogoutButton());
        }

        public void doubleClick(IWebDriver driver, IWebElement iWebElement)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(iWebElement).DoubleClick().Perform();
        }

        public static void WaitForElementToBeEnabled(IWebDriver driver,IWebElement element)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public static void WaitForElementNotToBeVisible(IWebDriver driver, IWebElement element)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.StalenessOf(element));
        }

        public static void WaitForTextInElement(IWebDriver driver, By locator, string text)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(locator, text));
        }

        public static void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public static void JavaScriptClick(IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor javascriptExecutor = (IJavaScriptExecutor)driver;
            javascriptExecutor.ExecuteScript("arguments[0].click();", element);
        }

        public static void WaitUntilAttributeChanges(IWebDriver driver, IWebElement element, string attributeName, string expectedValue)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditionsEx.AttributeContains(element, attributeName, expectedValue));
        }

        public static string getProjectDirectory()
        {
            string projectDirectory = Directory.GetCurrentDirectory();
            return Directory.GetParent(Directory.GetParent(Directory.GetParent(projectDirectory)?.FullName)?.FullName)?.FullName;         
        }

        // Custom class for ExpectedConditionsEx --> This method checks if the attribute value contains the expected value, and returns true if the condition is met.
        public static class ExpectedConditionsEx
        {
            public static Func<IWebDriver, bool> AttributeContains(IWebElement element, string attributeName, string expectedValue)
            {
                return driver =>
                {
                    string actualValue = element.GetAttribute(attributeName);
                    return actualValue != null && actualValue.Contains(expectedValue);
                };
            }
        }

        public static string GenerateRandomStringWithTodayDate(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string datePrefix = DateTime.Today.ToString("yyyyMMdd");
            string randomString = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return datePrefix + randomString;
        }

    }
}
