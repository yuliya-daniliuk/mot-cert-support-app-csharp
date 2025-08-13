
using RestAssured.Response;
using static RestAssured.Dsl;

namespace Timesheet.Tests.API;

public class APITestsRequests
{
    private static string host = "http://localhost:8080";

   public static VerifiableResponse PostLogin(string email, string password) {
       return Given()
               .Body("{\"email\":\"" + email + "\",\"password\":\"" + password + "\"}")
               .ContentType("application/json")
               .Post(host + "/v1/auth/login");
   }

   public static HttpResponseMessage PostValidate(string token) {
       return Given()
               .Body("{\"token\":\"" + token + "\"}")
               .ContentType("application/json")
               .Post(host + "/v1/auth/validate")
               .Then()
               .Extract()
               .Response();
   }

   public static HttpResponseMessage PostLogout(string token) {
       return Given()
               .Body("{\"token\":\"" + token + "\"}")
               .ContentType("application/json")
               .Post(host + "/v1/auth/logout")
               .Then()
               .Extract()
               .Response();
   }
}
