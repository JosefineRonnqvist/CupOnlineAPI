using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace CupOnlineAPI.Repositories
{
    public class PasswordRepository
    {
        /// <summary>
        /// the size of the salt, this in combination with the hashsize builds the password length
        /// increasing one will increase the other. With values of 15 for the salt and 20 for the hash 
        /// the password will be 48 characters in length.
        /// </summary>
        const int saltSize = 16;
        const int hashSize = 20;


        /// <summary>
        /// Uses Password-Based Key Derivation Function 2 (pbkdf2) / RFC2898, to generate the hash and random salt.
        /// </summary>
        /// <param name="password">the password to hash.</param>
        /// <returns>the hashed password as a base64 string</returns>
        public async Task<string> HashPassword(string password, int iterations = 100000)
        {
            byte[] salt = new byte[saltSize];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
                {
                    byte[] hash = pbkdf2.GetBytes(hashSize);
                    byte[] hashBytes = new byte[saltSize + hashSize];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);
                    string savedPasswordHash = Convert.ToBase64String(hashBytes);
                    return savedPasswordHash;
                }
            }
        }

        /// <summary>
        /// Compares a hashed password against the password input from the user. 
        /// Uses Password-Based Key Derivation Function 2 (pbkdf2) / RFC2898, to generate the hash and random salt.
        /// </summary>
        /// <param name="password">The password to compare against</param>
        /// <param name="savedPasswordHash">The hashed password</param>
        /// <returns>If there's any differences in the computed hash return false, 
        /// as this indicated that the passwords are not a match, otherwise return true.</returns>
        public bool VerifyPassword(string password, string savedPasswordHash, int iterations = 100000)
        {
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[saltSize];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(hashSize);

            for (int i=0; i<20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;           
        }

        /// <summary>
        /// Creates a char array with random chars from provided char options
        /// </summary>
        /// <param name="charOptions">Chars to be randomised</param>
        /// <param name="length">length of char array</param>
        /// <returns></returns>
        private string RandomChars(string charOptions, int length)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = charOptions;
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        /// <summary>
        /// Creates a password with randomised chars
        /// </summary>
        /// <returns>created password</returns>
        public async Task<string> RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomChars("ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-", 4));
            builder.Append(RandomChars("!@#$%^&*?_-", 1));
            builder.Append(RandomChars("ABCDEFGHJKLMNOPQRSTUVWXYZ", 1));
            builder.Append(RandomChars("0123456789", 1));
            builder.Append(RandomChars("abcdefghijklmnopqrstuvwxyz", 1));
            return builder.ToString();
        }
    }
}
