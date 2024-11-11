using ApprovalTests;
using static RestAssured.Dsl;
using Timesheet.Models.Auth;

namespace Timesheet.Tests.API;

public class APITests
{
    [Test]
   public void TestGettingProject2()
   {
        Credentials credentials = (Credentials)Given()
                                       .Body("{\"email\":\"admin@test.com\",\"password\":\"password123\"}")
                                       .ContentType("application/json")
                                       .Post("http://localhost:8080/v1/auth/login")
                                       .DeserializeTo(typeof (Credentials));
        string token = credentials.Token;
        
        HttpResponseMessage response = Given()
                                       .Header("Authorization", "Bearer " + token)
                                       .Get("http://localhost:8080/v1/project/2")
                                       .Then()
                                       .Extract()
                                       .Response();

        Approvals.Verify(response.Content.ReadAsStringAsync().Result);
   }

}
