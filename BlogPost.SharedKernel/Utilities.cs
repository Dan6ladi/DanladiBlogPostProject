using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlogPost.SharedKernel
{
    public class Utilities
    {
        public static class PasswordHasher
        {
            private static string GenerateRandomSalt()
            {
                return Guid.NewGuid().ToString();
            }

            public static string GeneratePasswordSalt(string firstName, string lastname)
            {
                var baseSalt = GenerateRandomSalt();
                var salt = new StringBuilder();

                salt.Append(firstName);
                salt.Append(baseSalt);
                salt.Append(lastname);
                return salt.ToString();
            }

            public static string HashPassword(string password, string salt)
            {
                using (var hasher = SHA512.Create())
                {
                    var passwordHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                    return Convert.ToBase64String(passwordHash);
                }
            }

        }

        public static string MaskedProperty(object request)
        {
            if (request == null)
            {
                return "request is null";
            }

            JObject jsonRequest = JObject.FromObject(request);
            JToken Password = jsonRequest.SelectToken($"..Password");
            if (Password != null)
            {
                Password.Replace("******");
            }

            string maskedRequest = jsonRequest.ToString();

            return maskedRequest;
        }
    }
}
