using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class Locators
    {
        // Xpath, CSS, id, classname, name, tagname

        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            // Not a good practice : Declare implicit wait 3 seconds globally
            // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

            // by id and name
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Name("password")).SendKeys("123456");

            // by css selector & xpath
            // tagname[attribute = 'value']
            // driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();

            // by xpath (should start //)
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            // Use explicit wait
            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(driver.FindElement(By.Id("signInBtn")), "Sign In"));

            string errorMessage = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errorMessage);

            // by linktest
            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            string extedUrl = "https://rahulshettyacademy.com/documents-request";
            string hrefAttr = link.GetAttribute("href");

            // validate url of the link text
            Assert.That(hrefAttr, Is.EqualTo(extedUrl));

            // Tick I agree checkbox
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();

            // Advanced css selector : .text-info span:nth-child(1) input (Use space to go to child element)
        }
    }
}
