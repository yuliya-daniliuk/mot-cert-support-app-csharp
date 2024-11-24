using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Timesheet.Tests.E2E;

public class E2ELoginTest
{
   [Test]
   public void LoginTest() {
       DataBuilder dataBuilder = new DataBuilder();
       TimesheetCredential credentials = dataBuilder.GetUserCredentials("admin");

       // Set up the WebDriver for Chrome using WebDriverManager.
       new DriverManager().SetUpDriver(new ChromeConfig());

       // Initialize a new instance of the ChromeDriver.
       IWebDriver _webDriver = new ChromeDriver();

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

        _webDriver.Close();
        _webDriver.Quit();

   }
}
