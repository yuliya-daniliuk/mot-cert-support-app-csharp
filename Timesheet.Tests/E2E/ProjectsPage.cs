namespace Timesheet.Tests.E2E;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

// Declaration of the "ProjectsPage" class.
public class ProjectsPage
{

   // Store a reference to the WebDriver instance
   private IWebDriver _webDriver;

   // Constructor for the "ProjectsPage" class, which takes a WebDriver as a parameter.
   public ProjectsPage(IWebDriver _webDriver)
   {
       this._webDriver = _webDriver;

       // Initialize elements on the web page using the PageFactory pattern.
       PageFactory.InitElements(_webDriver, this);
      
       // Create a WebDriverWait instance to wait for a specific condition on the web page.
       WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(1000));

       // Wait until an element with the CSS selector ".card-title" becomes visible on the page.
       wait.Until(drv => drv.FindElement(By.CssSelector(".card-title")));
   }

   // Annotating a private WebElement field named "title" that represents an HTML element.
   [FindsBy(How = How.CssSelector, Using = ".card-title")]
   [CacheLookup]
   private IWebElement title;

   // A public method named "getTitle" that returns the text of the "title" WebElement.
   public string GetTitle() {
       return title.Text;
   }

}