using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumFramework.Utilities;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumFramework.Old.Tests
{
    [Parallelizable(ParallelScope.Self)]
    public class WindowHandlers
    {
        IWebDriver driver;

        [SetUp]

        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        //[Test]
        public void WindowHandle()
        {
            string email = "mentor@rahulshettyacademy.com";
            string parentWindowId = driver.CurrentWindowHandle;
            driver.FindElement(By.ClassName("blinkingText")).Click();

            Assert.AreEqual(2, driver.WindowHandles.Count);  //1

            string childWindowName = driver.WindowHandles[1];

            driver.SwitchTo().Window(childWindowName);

            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector(".red")).Text);
            string text = driver.FindElement(By.CssSelector(".red")).Text;

            // Please email us at mentor @rahulshettyacademy.com with below template to receive response

            string[] splittedText = text.Split("at");

            string[] trimmedString = splittedText[1].Trim().Split(" ");

            Assert.AreEqual(email, trimmedString[0]);

            driver.SwitchTo().Window(parentWindowId);

            driver.FindElement(By.Id("username")).SendKeys(trimmedString[0]);
            driver.Quit();
        }
    }
}
