using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    internal class SeleniumFirst
    {
        IWebDriver driver;

        [SetUp] public void StartBrowser()
        {
            // Set Webdrivermanager to overcome version management
            // Create object for ChromeDriver
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            // Firefox
            // new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            // driver = new FirefoxDriver();

            // Edge
            // new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            // driver = new EdgeDriver();

            driver.Manage().Window.Maximize();
        }
        
        [Test] public void Test1()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            TestContext.Progress.WriteLine(driver.Title);
            TestContext.Progress.WriteLine(driver.Url);            
        }

        [TearDown] public void TearDown()
        {
            driver.Close(); // Close the current window
            // driver.Quit(); // Close all windows
        }
    }
}
