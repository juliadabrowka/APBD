﻿using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if ((string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName)) || !email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
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
            var userCreditService = new UserCreditService();
            int creditLimit;

            switch (client.Type)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                    creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                    break;
                default:
                   user.HasCreditLimit = true;
                   creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                   user.CreditLimit = creditLimit;
                    break;
            }
            
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}