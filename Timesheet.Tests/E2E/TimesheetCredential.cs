namespace Timesheet.Tests.E2E;

public class TimesheetCredential
{

   public string Id { get; set; } = string.Empty;
   public string Username { get; set; } = string.Empty;
   public string Email { get; set; } = string.Empty;
   public string Password { get; set; } = string.Empty;
   public string Role { get; set; } = string.Empty;

   public TimesheetCredential(string username, string email, string password, string role)
   {
       Username = username;
       Email = email;
       Password = password;
       Role = role;
   }

}
