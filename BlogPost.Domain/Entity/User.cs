using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlogPost.SharedKernel.Enumerations;

namespace BlogPost.Domain.Entity
{
    public class User
    {
        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string Password { get; private set; }
        public string PasswordSalt { get; private set; }
        public string UserName { get; private set; }
        public SignUpType SignUpType { get; private set; }
        public DateTime DateRegistered { get; private set; }

        public User() { }


        public User(Guid id, string firstName, string lastName, string emailAddress, string password, string passwordSalt, string userName, SignUpType signUpType)
        {
            Id = id.ToString();
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Password = password;
            PasswordSalt = passwordSalt;
            UserName = userName;
            SignUpType = signUpType;
            DateRegistered = DateTime.Now;
        }

        public User(Guid id, string emailAddress, string userName, SignUpType signUpType)
        {
            Id = id.ToString();
            EmailAddress = emailAddress;
            UserName = userName;
            SignUpType = signUpType;
            DateRegistered = DateTime.Now;
        }

        public static User CreateUser(string firstName, string lastName, string emailAddress, string Password, string passwordSalt, string userName, SignUpType signUpType)
        {
            return new(Guid.NewGuid(),firstName,lastName,emailAddress,Password,passwordSalt,userName,signUpType);
        }

        public static User CreateUser(string emailAddress, string userName, SignUpType signUpType)
        {
            return new(Guid.NewGuid(), emailAddress, userName, signUpType);
        }
    }
}
