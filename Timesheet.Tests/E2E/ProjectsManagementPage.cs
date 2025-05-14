namespace Timesheet.Tests.E2E;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

public class ProjectsManagementPage
{
  WebDriverWait wait;
  private IWebDriver _webDriver;

  public ProjectsManagementPage(IWebDriver _webDriver) {
    this._webDriver = _webDriver;
    PageFactory.InitElements(_webDriver, this);

       wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
       wait.Until(drv => drv.FindElement(By.CssSelector(".card-title")));
  }

  [FindsBy(How = How.CssSelector, Using = "#name")]
  [CacheLookup]
  private IWebElement inputProjectName;

  [FindsBy(How = How.CssSelector, Using = "#description")]
  [CacheLookup]
  private IWebElement inputProjectDescription;

  [FindsBy(How = How.CssSelector, Using = ".btn-primary")]
  [CacheLookup]
  private IWebElement buttonAddProject;

  [FindsBy(How = How.CssSelector, Using = ".table-striped tbody tr")]
  private IList<IWebElement> projectsList;

  public void SendProjectName(string projectName) {
      inputProjectName.SendKeys(projectName);
  }

  public void SendProjectDescription(string projectDescription) {
      inputProjectDescription.SendKeys(projectDescription);
  }

  public void ClickAddProject() {
       int initalCount = projectsList.Count;

       buttonAddProject.Click();

       wait.Until(drv => drv.FindElements(By.CssSelector(".table-striped tbody tr")).Count == initalCount + 1);
  }

  public IList<IWebElement> GetProjectsList() {
      return projectsList;
  }
 
}
