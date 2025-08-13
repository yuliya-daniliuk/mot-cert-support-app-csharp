using ApprovalTests;
using static RestAssured.Dsl;
using Timesheet.Models.Auth;
using RestAssured.Request.Logging;
using RestAssured.Response.Logging;
using NUnit.Allure.Core;

namespace Timesheet.Tests.API;
[AllureNUnit]
public class APITests
{
     [SetUp]
     public void Setup()
     {          
          RestAssuredConfig.RequestLogLevel = RequestLogLevel.All;
          RestAssuredConfig.ResponseLogLevel = ResponseLogLevel.All;
          
     }
     
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

   [Test]
   public void TestLoginReturnsPositiveResponse(){
       Login loginPayload = new Login("admin@test.com", "password123");
       HttpResponseMessage response = APITestsRequests.PostLogin(loginPayload).Then().Extract().Response();

       Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
   }

   [Test]
   public void TestLoginReturnsNegativeResponse() {
       Login loginPayload = new Login("incorrect@test.com", "password123");
       HttpResponseMessage response = APITestsRequests.PostLogin(loginPayload).Then().Extract().Response();


       Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Unauthorized));
   }

   [Test]
   public void TestValidateReturnsPositiveResponse(){
       Login loginPayload = new Login("admin@test.com", "password123");
       Credentials credentials = (Credentials)APITestsRequests.PostLogin(loginPayload).DeserializeTo(typeof (Credentials));

       Token tokenPayload = new Token(credentials.Token);
       HttpResponseMessage response = APITestsRequests.PostValidate(tokenPayload);

       Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
   }

   [Test]
   public void TestValidateReturnsNegativeResponse(){
       Token tokenPayload = new Token("321cba");
       HttpResponseMessage response = APITestsRequests.PostValidate(tokenPayload);

       Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Unauthorized));
   }

   [Test]
   public void TestLogoutReturnPositiveResponse(){
       Login loginPayload = new Login("admin@test.com", "password123");
       Credentials credentials = (Credentials)APITestsRequests.PostLogin(loginPayload)
                                       .DeserializeTo(typeof (Credentials));

       Token tokenPayload = new Token(credentials.Token);
       HttpResponseMessage response = APITestsRequests.PostLogout(tokenPayload);

       Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Accepted));
   }

}
