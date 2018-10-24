using System;
using System.Security.Cryptography;
using System.Text;

namespace INV2019
{
    public class SecretPerson
    {
        private const string PUBLIC_PASSWORD = @"W&k}^5,I|=^v3W*:2i(v8123";
        private const string PUBLIC2_PASSWORD = @"SsXa8b}Y;Y8&PR09gYRkd345";
        private const string COMBINED_PASSWORD = @"RnxB+!lig3L5sFk7S1my567";
        private const char SPLIT = '?';

        public static string EncryptString(string Message)
        {
            var key = DateTime.Now.Ticks.ToString();
            var message = EncryptString(Message, key);
            message = CombineString(message, key);
            return EncryptString(message, PUBLIC_PASSWORD);
        }

        public static string DecryptString(string Message)
        {
            var ds = DecryptString(Message, PUBLIC_PASSWORD);
            var des = DecombineString(ds);
            var str = DecryptString(des[0], des[1]);
            return str;
        }

        public static string CombineString(params string[] strs)
        {
            if (strs != null)
            {
                var sb = new StringBuilder();
                foreach (var str in strs)
                    sb.Append(SPLIT + EncryptString(str, COMBINED_PASSWORD));
                if (sb.Length > 0)
                    sb = sb.Remove(0, 1);

                return EncryptString(sb.ToString(), PUBLIC2_PASSWORD);
            }

            return string.Empty;
        }

        public static string[] DecombineString(string str)
        {
            var message = DecryptString(str, PUBLIC2_PASSWORD);
            var strs = message.Split(SPLIT);

            if (strs != null && strs.Length > 0)
            {
                var tmpStrs = new string[strs.Length];
                for (int i = 0; i < strs.Length; i++)
                {
                    tmpStrs[i] = DecryptString(strs[i], COMBINED_PASSWORD);
                }
                return tmpStrs;
            }

            return new string[] { };
        }

        public static string EncryptString(string Message, string Passphrase)
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

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

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

        public static string DecryptString(string Message, string Passphrase)
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
            byte[] DataToDecrypt = Convert.FromBase64String(Message);

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
