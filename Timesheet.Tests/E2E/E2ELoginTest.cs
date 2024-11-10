namespace Timesheet.Tests.E2E;

public class E2ELoginTest
{
   [Test]
   public void LoginTest() {
       DataBuilder dataBuilder = new DataBuilder();
       TimesheetCredential credentials = dataBuilder.GetUserCredentials("admin");
   }
}
