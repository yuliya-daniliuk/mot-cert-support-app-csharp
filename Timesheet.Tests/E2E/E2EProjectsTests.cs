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

    [SetUp]
   public void SetUp() {
       new DriverManager().SetUpDriver(new ChromeConfig());

       /* ChromeOptions options = new ChromeOptions();
       options.AddArguments("--headless");
       _webDriver = new ChromeDriver(options); - for some reason adding options here breaks the second test */
       _webDriver = new ChromeDriver();

       _webDriver.Navigate().GoToUrl("http://localhost:8080");

       LoginPage loginPage = new LoginPage(_webDriver);
       loginPage.SendEmail("admin@test.com");
       loginPage.SendPassword("password123");
       loginPage.SubmitForm();
   }
    
   [Test]
    public void TestProjectListIsShownOnPage()
    {
        DataBuilder dataBuilder = new DataBuilder();
        TimesheetCredential credentials = dataBuilder.GetUserCredentials("admin");

        ProjectsPage projectsPage = new ProjectsPage(_webDriver);
        Assert.IsTrue(projectsPage.GetTitle() == "Projects");

        _webDriver.FindElement(By.CssSelector("a[href='#/manage/projects']")).Click();
        Thread.Sleep(1000);

        ReadOnlyCollection<IWebElement> projects = _webDriver.FindElements(By.CssSelector("tbody tr"));
        Assert.That(projects.Count, Is.GreaterThan(0));
    }

    [Test]
    public void testAddingANewProject()
    {
        DataBuilder dataBuilder = new DataBuilder();
        TimesheetCredential credentials = dataBuilder.GetUserCredentials("admin");

        ProjectsPage projectsPage = new ProjectsPage(_webDriver);
        projectsPage.ClickManageProject();

        ProjectsManagementPage projectsManagementPage = new ProjectsManagementPage(_webDriver);
        int initialCount = projectsManagementPage.GetProjectsList().Count;

        projectsManagementPage.SendProjectName("New Project");
        projectsManagementPage.SendProjectDescription("This is a new project");
        projectsManagementPage.ClickAddProject();

        IList<IWebElement> projects = projectsManagementPage.GetProjectsList();
        Assert.That(projects.Count, Is.EqualTo(initialCount + 1));
    }
    
    [TearDown]
    public void TearDown() 
    {
        TestContext currentContext = TestContext.CurrentContext;
        Console.WriteLine($"we are in teardown not checked for null driver {_webDriver.Title}");

        if (_webDriver != null) 
        {
            Console.WriteLine("we are in teardown");
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
