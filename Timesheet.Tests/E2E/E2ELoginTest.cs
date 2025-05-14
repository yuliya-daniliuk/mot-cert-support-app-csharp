using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using DevToolsSessionDomains = OpenQA.Selenium.DevTools.V130.DevToolsSessionDomains; 
using Network = OpenQA.Selenium.DevTools.V130.Network;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Support.Extensions;
using NUnit.Allure.Core;
using Allure.Net.Commons;

namespace Timesheet.Tests.E2E;

[AllureNUnit]
public class E2ELoginTest
{
    private IWebDriver _webDriver; 

   [Test]
   public void AdminCanAuth() {
       DataBuilder dataBuilder = new DataBuilder();
       TimesheetCredential credentials = dataBuilder.GetUserCredentials("admin");

       // Set up the WebDriver for Chrome using WebDriverManager.
       new DriverManager().SetUpDriver(new ChromeConfig());

       // Initialize a new instance of the ChromeDriver.
       _webDriver = new ChromeDriver();

    //    var devTools = _webDriver as IDevTools;
    //    var session = devTools.GetDevToolsSession();
    //    var domains = session.GetVersionSpecificDomains<DevToolsSessionDomains>();
    //    domains.Network.Enable(new Network.EnableCommandSettings());
    //    Network.EmulateNetworkConditionsCommandSettings command = new Network.EmulateNetworkConditionsCommandSettings
    //     {
    //         Latency = 1000,
    //     };
    //     domains.Network.EmulateNetworkConditions(command);

       // Open a web page with the given URL in the Chrome browser.
       _webDriver.Navigate().GoToUrl("http://localhost:8080");
       
       // Create an instance of the LoginPage class, which is a custom class.
       LoginPage loginPage = new LoginPage(_webDriver);

       // Perform actions on the login page: sending email, password, and submitting the form.
       loginPage.SendEmail(credentials.Email);
       loginPage.SendPassword(credentials.Password);
       loginPage.SubmitForm();

        ProjectsPage projectsPage = new ProjectsPage(_webDriver);
        Assert.IsTrue(projectsPage.GetTitle() == "Projects");

   }

   [Test]
   public void StanCanAuth() 
   {
    DataBuilder dataBuilder = new DataBuilder();
    TimesheetCredential credentials = dataBuilder.GetUserCredentials("user");

       // Set up the WebDriver for Chrome using WebDriverManager.
       new DriverManager().SetUpDriver(new ChromeConfig());

       // Initialize a new instance of the ChromeDriver.
       _webDriver = new ChromeDriver();

    //    var devTools = _webDriver as IDevTools;
    //    var session = devTools.GetDevToolsSession();
    //    var domains = session.GetVersionSpecificDomains<DevToolsSessionDomains>();
    //    domains.Network.Enable(new Network.EnableCommandSettings());
    //    Network.EmulateNetworkConditionsCommandSettings command = new Network.EmulateNetworkConditionsCommandSettings
    //     {
    //         Latency = 1000,
    //     };
    //     domains.Network.EmulateNetworkConditions(command);

       // Open a web page with the given URL in the Chrome browser.
       _webDriver.Navigate().GoToUrl("http://localhost:8080");
       
       // Create an instance of the LoginPage class, which is a custom class.
       LoginPage loginPage = new LoginPage(_webDriver);

       // Perform actions on the login page: sending email, password, and submitting the form.
       loginPage.SendEmail(credentials.Email);
       loginPage.SendPassword(credentials.Password);
       loginPage.SubmitForm();

        ProjectsPage projectsPage = new ProjectsPage(_webDriver);
        Assert.IsTrue(projectsPage.GetTitle() == "Projects");
   }

   [TearDown]
   public void TearDown() 
   {
        TestContext currentContext = TestContext.CurrentContext;

        if (_webDriver != null) 
        {
            if (currentContext.Result.Outcome != ResultState.Success)
            {
                var screenshotPath = $"{currentContext.Test.Name}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss.fffff}.png";
                _webDriver.TakeScreenshot().SaveAsFile(screenshotPath);
                TestContext.AddTestAttachment(screenshotPath, "Screenshot");
                AllureApi.AddAttachment(screenshotPath, currentContext.Test.Name);
            }
        _webDriver.Close();
        _webDriver.Quit();

        }
        
    }
}
