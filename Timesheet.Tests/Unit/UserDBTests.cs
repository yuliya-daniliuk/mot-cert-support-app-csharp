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

}
