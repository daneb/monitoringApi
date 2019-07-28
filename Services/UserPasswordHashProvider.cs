using System;
using Services.Helpers;
using Services.Interfaces;

namespace Services
{
    public class UserPasswordHashProvider : IUserPasswordHashProvider
    {

        public const string PassPhrase = "@#$^asdv";

        /// <summary>
        /// Returns user password hash.
        /// </summary>
        public string Hash(string password)
        {
            return Encryption.Encrypt(password, PassPhrase);
        }
    }
}
