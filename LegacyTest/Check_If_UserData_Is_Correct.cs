using LegacyApp;

namespace LegacyTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CheckIfUserDataIsCorrect()
    {
        string name = "robert";
        string surname = "addams";
        string email = "abc";
        DateTime birth = new DateTime(1995, 12, 25);
        int id = 1;
        var service = new UserService();
        
        Assert.Throws<Exception>(() => service.AddUser(name, surname, email, birth, id), "Given data is incorrect. Please review it");

        name = "";
        email = "abc@gmail.com";
        
        Assert.Throws<Exception>(() => service.AddUser(name, surname, email, birth, id), "User is not allowed to open an account");

        name = "julia";
        surname = "";
        
        Assert.Throws<Exception>(() => service.AddUser(name, surname, email, birth, id), "Given data is incorrect. Please review it");
    }
}