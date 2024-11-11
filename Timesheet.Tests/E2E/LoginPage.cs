namespace Timesheet.Tests.E2E;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

// Create a class named 'LoginPage'
public class LoginPage
{

   // Create a constructor for the LoginPage class
   public LoginPage(IWebDriver _webDriver)
   {
       PageFactory.InitElements(_webDriver, this);
      
       WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
       wait.Until(drv => drv.FindElement(By.CssSelector("button")));
   }

   // Define WebElements for email, password, and submit button
   [FindsBy(How = How.Name, Using = "email")]
   [CacheLookup]
   private IWebElement email;

   [FindsBy(How = How.Name, Using = "password")]
   [CacheLookup]
   private IWebElement password;

   [FindsBy(How = How.CssSelector, Using = "button")]
   [CacheLookup]
   private IWebElement submit;

   // Method to input an email address into the email field
   public void SendEmail(String value) {
       email.SendKeys(value);
   }

   // Method to input a password into the password field
   public void SendPassword(String value) {
       password.SendKeys(value);
   }

   // Method to submit the login form
   public void SubmitForm() {
       submit.Click();
   }

}
