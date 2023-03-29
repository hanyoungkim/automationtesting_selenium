using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Interactions;

namespace SeleniumLearning
{
    public class AlertsActionsAutoSuggestive
    {
        IWebDriver driver;

        //[SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
        }

        //[Test]
        public void test_Alert()
        {
            string name = "Lucas";
            driver.FindElement(By.CssSelector("#name")).SendKeys(name);
            driver.FindElement(By.CssSelector("input[onclick*='displayConfirm']")).Click();
            string alertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept(); // Ok
            // driver.SwitchTo().Alert().Dismiss(); // Cancel
            StringAssert.Contains(name,alertText);
        }

        //[Test]
        public void test_AutoSuggestiveDropDowns()
        {
            driver.FindElement(By.Id("autocomplete")).SendKeys("ind");
            Thread.Sleep(3000);

            IList<IWebElement> options = driver.FindElements(By.CssSelector(".ui-menu-item div"));

            foreach (IWebElement option in options)
            {
                if (option.Text.Equals("India"))
                {
                    option.Click();
                }
            }

            // Runtime 동안 입력된 값들은 .text 로 찾을 수 없음 / 이럴 땐 getattrivute("Value")로 찾아야 함
            TestContext.Progress.WriteLine(driver.FindElement(By.Id("autocomplete")).GetAttribute("value"));
        }


        //[Test]
        public void test_Acutions()
        {
            driver.Url = "https://rahulshettyacademy.com/";

            Actions a = new Actions(driver);
            a.MoveToElement(driver.FindElement(By.CssSelector("a.dropdown-toggle"))).Perform();

            driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[1]/a")).Click();

            // a.DragAndDrop(a,b).Perform(); // Drag and drop
            // a.ContextClick(); // right mouse click
        }

        //[Test]
        public void frames()
        {
            // scroll
            IWebElement frameScroll =  driver.FindElement(By.Id("courses-iframe"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)",frameScroll);

            // id, name, index
            driver.SwitchTo().Frame("course-iframe");
            driver.FindElement(By.LinkText("All Access Plan")).Click();

            // Go back to parent screen(page)
            driver.SwitchTo().DefaultContent();
        }
    }
}
