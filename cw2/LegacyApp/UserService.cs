using System;

namespace LegacyApp;

public class UserService
{ 
    public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        if (UserDataValidator(firstName, lastName, email))
        {
            throw new Exception("Given data is incorrect. Please review it");
        }

        if (UserIsNotAdult(dateOfBirth))
        {
            throw new Exception("User is not allowed to open an account");
        }

        var clientRepository = new ClientRepository();
        var client = clientRepository.GetById(clientId);

        var user = new User
        {
            Client = client,
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            FirstName = firstName,
            LastName = lastName
        };
        
        if(!AssignLimit(client.Type, user))
        {
            throw new Exception("Credit limit can not be granted");
        }

        UserDataAccess.AddUser(user);
        return true;
    }

    private bool UserIsNotAdult(DateTime userBirthDate)
    {
        var now = DateTime.Now;
        int age = now.Year - userBirthDate.Year;
        if (now.Month < userBirthDate.Month || (now.Month == userBirthDate.Month && now.Day < userBirthDate.Day)) age--;

        return age < 21;
    }

    private bool UserDataValidator(string firstName, string lastName, string email)
    {
        return (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName)) ||
               !email.Contains("@") && !email.Contains(".");
    }

    private bool AssignLimit(ClientImportanceType clientType, User user)
    {
        var userCreditService = new UserCreditService();
        int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
        
        switch (clientType)
        {
            case ClientImportanceType.VeryImportantClient:
                user.HasCreditLimit = false;
                break;
            case ClientImportanceType.ImportantClient:
                creditLimit *= 2;
                user.CreditLimit = creditLimit;
                break;
            default:
                user.HasCreditLimit = true;
                user.CreditLimit = creditLimit;
                break;
        }
        
        if (user.HasCreditLimit && user.CreditLimit < 500)
        {
            return false;
        }

        return true;
    }
}
