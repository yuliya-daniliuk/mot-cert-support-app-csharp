
using RestAssured.Response;
using Timesheet.Models.Auth;
using static RestAssured.Dsl;

namespace Timesheet.Tests.API;

public class APITestsRequests
{
    private static string host = "http://localhost:8080";

   public static VerifiableResponse PostLogin(Login login) {
       return Given()
               .Body(login)
               .ContentType("application/json")
               .Post(host + "/v1/auth/login");
   }

   public static HttpResponseMessage PostValidate(Token token) {
       return Given()
               .Body(token)
               .ContentType("application/json")
               .Post(host + "/v1/auth/validate")
               .Then()
               .Extract()
               .Response();
   }

   public static HttpResponseMessage PostLogout(Token token) {
       return Given()
               .Body(token)
               .ContentType("application/json")
               .Post(host + "/v1/auth/logout")
               .Then()
               .Extract()
               .Response();
   }
}
