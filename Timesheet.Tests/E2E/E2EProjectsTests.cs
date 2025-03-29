namespace Timesheet.Tests.E2E;

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
using System.Collections.ObjectModel;

[AllureNUnit]
public class E2EProjectsTests
{
    private IWebDriver _webDriver; 
    
   [Test]
    public void TestProjectListIsShownOnPage()
    {
        DataBuilder dataBuilder = new DataBuilder();
        TimesheetCredential credentials = dataBuilder.GetUserCredentials("admin");

        new DriverManager().SetUpDriver(new ChromeConfig());

        ChromeOptions options = new ChromeOptions();
        ///options.AddArguments("--headless");

        IWebDriver _webDriver = new ChromeDriver(options);

        _webDriver.Navigate().GoToUrl("http://localhost:8080");

        // Create an instance of the LoginPage class, which is a custom class.
       LoginPage loginPage = new LoginPage(_webDriver);

       // Perform actions on the login page: sending email, password, and submitting the form.
       loginPage.SendEmail(credentials.Email);
       loginPage.SendPassword(credentials.Password);
       loginPage.SubmitForm();

        ProjectsPage projectsPage = new ProjectsPage(_webDriver);
        Assert.IsTrue(projectsPage.GetTitle() == "Projects");

        _webDriver.FindElement(By.CssSelector("a[href='#/manage/projects']")).Click();

        Thread.Sleep(1000);

        ReadOnlyCollection<IWebElement> projects = _webDriver.FindElements(By.CssSelector("tbody tr"));

        Assert.That(projects.Count, Is.GreaterThan(0));

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
