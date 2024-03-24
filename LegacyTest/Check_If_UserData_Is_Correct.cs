using LegacyApp;

namespace LegacyTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Check_If_UserData_Is_Correct()
    {
        // data
        string name = "robert";
        string surname = "addams";
        string email = "abc";
        DateTime birth = new DateTime(1995, 12, 25);
        int id = 1;
        var service = new UserService();
        
        // service call
        bool isUserAdded = service.AddUser(name, surname, email, birth, id);
        
        //assert
        Assert.AreEqual(false, isUserAdded);
    }

    [Test]
    public void Check_If_User_Name_Is_Correct()
    {
        //for empty name
        string emptyName = "";
        string surname = "addams";
        string email = "abc@gmail.com";
        DateTime birth = new DateTime(1995, 12, 25);
        int id = 1;
        var service = new UserService();
        
        // service call
        bool isUserAddedWithEmptyName = service.AddUser(emptyName, surname, email, birth, id);
        
        //assert
        Assert.AreEqual(false, isUserAddedWithEmptyName);
        
        // for empty surmane
        string name = "alice";
        string emptySurname = "";
        
        // service call
        bool isUserAddedWithEmptySurname = service.AddUser(name, emptyName, email, birth, id);
        
        //assert
        Assert.AreEqual(false, isUserAddedWithEmptySurname);
    }
}