namespace Timesheet.Tests.Unit;

using ApprovalTests;
using Timesheet.DB;
using Timesheet.Models.User;

public class UserDBTests
{
    private static UserDB _userDB;

    [SetUp]
    public static void Setup()
    {
        PrepareDB prepareDB = new PrepareDB();
        prepareDB.SeedUsers();
        _userDB = new UserDB();
    }

    [Test]
    public void UserCreatedSuccessfully()
    {
        User user = new User("Jon", "test@email.com", "password", "user");
        User result = _userDB.CreateUser(user);

        Assert.That(result.Username == user.Username);
        Assert.That(result.Email == user.Email);
        Assert.That(result.Password == user.Password);
        Assert.That(result.Role == user.Role);

    }

    [Test]
    public void UserUpdatedSuccessfully()
    {
        User user = new User("Jon", "test@email.com", "password", "user");
        User result = _userDB.CreateUser(user);

        User updatedFields = new User("JonUpd", "testUpd@email.com", "passwordUpd", "admin");
        bool updatedResult = _userDB.UpdateUser(result.Id, updatedFields);
        Assert.That(updatedResult == true);

        User updatedUser = _userDB.GetUserProfile(result.Id);
        Assert.That(updatedUser.Username == updatedFields.Username);
        Assert.That(updatedUser.Email == updatedFields.Email);
        Assert.That(updatedUser.Password == updatedFields.Password);
        Assert.That(updatedUser.Role == updatedFields.Role);
    }

    [Test]
    public void UserDeletedSuccessfully()
    {
        bool result = _userDB.DeleteUser(1);

        Assert.That(result, Is.True);
        Assert.That(_userDB.GetUserProfile(1) == null);
    }

    [Test]
    public void NonUserCannotBeDeleted()
    {
        List<User> listBefore = _userDB.GetUsers();
        int countBefore = listBefore.Count;

        bool result = _userDB.DeleteUser(countBefore+1);

        Assert.That(result, Is.False);
        Assert.That(_userDB.GetUsers().Count == countBefore);
    }

    [Test]
    public void TestUserList() {
        User user = new User("Jon", "test@email.com", "password", "user");
        _userDB.CreateUser(user);
        User user2 = new User("Jon", "test@email.com", "password", "user");
        _userDB.CreateUser(user);
        User user3 = new User("Jon", "test@email.com", "password", "user");
        _userDB.CreateUser(user);

        List<User> users = _userDB.GetUsers();
        List<string> usersList = users.Select(user => user.ToString()).ToList();
        string allUsers = string.Join("\n", usersList);
        Approvals.Verify(allUsers);

    }

    [Test]
    public void GetUserProfileSuccessfully() 
    {
        User admin = _userDB.GetUserProfile(1);
        Assert.That(admin.Username == "admin");
        Assert.That(admin.Email == "admin@test.com");
        Assert.That(admin.Password == "password123");
        Assert.That(admin.Role == "admin");
    }

    [Test]
    public void CannotGetNonUserProfile() 
    {
        List<User> listBefore = _userDB.GetUsers();
        int countBefore = listBefore.Count;

        User result = _userDB.GetUserProfile(countBefore+1);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void CannotUpdatedNonUser()
    {
        List<User> usersBefore = _userDB.GetUsers();
        User updatedFields = new User("JonUpd", "testUpd@email.com", "passwordUpd", "admin");
        List<User> listBefore = _userDB.GetUsers();
        int countBefore = listBefore.Count;
        
        bool updatedResult = _userDB.UpdateUser(countBefore+1, updatedFields);
        Assert.That(updatedResult == false);
        List<User> usersAfter = _userDB.GetUsers();

        var counter = 0;
        foreach (User user in usersBefore) 
        {
            var str = user.ToString() + " to " + usersAfter[counter].ToString();
            Console.WriteLine("Comparing " + str);
            Assert.That(user.ToString() == usersAfter[counter].ToString());
            counter++;
        }       
    }
}
