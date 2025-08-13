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
       HttpResponseMessage response = APITestsRequests.PostLogin("admin@test.com", "password123").Then().Extract().Response();

       Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
   }

   [Test]
   public void TestLoginReturnsNegativeResponse() {
       HttpResponseMessage response = APITestsRequests.PostLogin("incorrect@test.com", "password123").Then().Extract().Response();


       Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Unauthorized));
   }

   [Test]
   public void TestValidateReturnsPositiveResponse(){
       Credentials credentials = (Credentials)APITestsRequests.PostLogin("admin@test.com", "password123").DeserializeTo(typeof (Credentials));

       HttpResponseMessage response = APITestsRequests.PostValidate(credentials.Token);

       Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
   }

   [Test]
   public void TestValidateReturnsNegativeResponse(){
       HttpResponseMessage response = APITestsRequests.PostValidate("321cba");

       Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Unauthorized));
   }

   [Test]
   public void TestLogoutReturnPositiveResponse(){
       Credentials credentials = (Credentials)APITestsRequests.PostLogin("admin@test.com", "password123")
                                       .DeserializeTo(typeof (Credentials));

       HttpResponseMessage response = APITestsRequests.PostLogout(credentials.Token);

       Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Accepted));
   }

}
