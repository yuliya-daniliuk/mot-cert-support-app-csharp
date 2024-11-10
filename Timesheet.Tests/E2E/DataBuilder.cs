namespace Timesheet.Tests.E2E;

using System;
using Timesheet.Models.Auth;

using static RestAssured.Dsl;

public class DataBuilder
{

   internal string token;
   internal string host = "http://localhost:8080";

   public DataBuilder() {
       Credentials credentials = (Credentials)Given()
               .Body("{\"email\":\"admin@test.com\",\"password\":\"password123\"}")
               .ContentType("application/json")
               .Post(host + "/v1/auth/login")
               .DeserializeTo(typeof(Credentials));

       token = credentials.Token;
   }

   public TimesheetCredential GetUserCredentials(string userType) {
       List<TimesheetCredential> credentials = GetStandardUser();

       foreach(TimesheetCredential credential in credentials){
           if(credential.Role.Equals(userType)){
               return credential;
           }
       }

       TimesheetCredential createdCredentials = CreateStandardUser(userType);

       return createdCredentials;
   }
  
   private TimesheetCredential CreateStandardUser(string userType)
   {
       TimesheetCredential credential = (TimesheetCredential)Given()
                                       .Body(new TimesheetCredential("Jon", "test@email.com", "password123", userType))
                                       .ContentType("application/json")
                                       .Header("Authorization", "Bearer " + token)
                                       .Post("/v1/user")
                                       .DeserializeTo(typeof(TimesheetCredential));

       return credential;
   }

   private List<TimesheetCredential> GetStandardUser() {
       return (List<TimesheetCredential>)Given()
               .Header("Authorization", "Bearer " + token)
               .Get(host + "/v1/user")
               .DeserializeTo(typeof(List<TimesheetCredential>));
   }


}
