using System;
using System.Security.Cryptography;

namespace Services.Helpers
{
    /// <summary>
    /// Encryption Helpder
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// prefix marker for strings that are encrypted
        /// </summary>
        private const string EncryptPrefix = "enc:";

        /// <summary>
        /// Encrypt a clear text string using a particular passphrase.
        /// The output encrypted string is prefixed with 'enc:'.
        /// </summary>
        public static string Encrypt(string clearString, string passphrase)
        {
            var encrypted = EncryptString(clearString, passphrase);
            return $"{Encryption.EncryptPrefix}{encrypted}";
        }

        /// <summary>
        /// Decrypt an encrypted string prefixed with 'enc:' using a particular passphrase.
        /// If the input string is not prefixed with 'enc:' then simply returns the input string as is.
        /// </summary>
        public static string Decrypt(string encryptedString, string passphrase)
        {
            if (string.IsNullOrEmpty(encryptedString))
                return encryptedString;

            if (!encryptedString.StartsWith(Encryption.EncryptPrefix))
                return encryptedString; // nothing to decrypt if incorrect prefix

            var encrypted = encryptedString.Remove(0, Encryption.EncryptPrefix.Length);
            return Encryption.DecryptString((string)encrypted, passphrase);
        }



        private static string EncryptString(string message, string passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }

        private static string DecryptString(string message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
    }
}