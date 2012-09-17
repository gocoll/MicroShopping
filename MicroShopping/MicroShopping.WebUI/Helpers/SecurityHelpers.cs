using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace MicroShopping.WebUI.Helpers
{
    public static class SecurityHelpers
    {
        public static string GenerateRandomToken()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            char ch;

            for (int i = 0; i < 64; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Hash a password using the BCrypt encryption algorithm.
        /// </summary>
        /// <param name="password">The cleartext password.</param>
        /// <returns>A hashed string of the password using BCrypt.</returns>
        public static string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(6);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword;
        }
    }
}