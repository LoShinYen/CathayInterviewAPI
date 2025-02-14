using System.Security.Cryptography;
using System.Text;

namespace CathayInterviewAPI.Helpers
{
    public static class EncryptionHelper
    {
        private static readonly string _key = "YourSecretKey123456YourSecretKey12";

        private static byte[] GetAesKey()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(_key)).Take(32).ToArray();
            }
        }

        public static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = GetAesKey();
                aes.GenerateIV();
                byte[] iv = aes.IV;
                using (var encryptor = aes.CreateEncryptor(aes.Key, iv))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    return Convert.ToBase64String(iv.Concat(encryptedBytes).ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes aes = Aes.Create())
                {
                    aes.Key = GetAesKey();
                    byte[] iv = cipherBytes.Take(aes.BlockSize / 8).ToArray();
                    byte[] encryptedBytes = cipherBytes.Skip(aes.BlockSize / 8).ToArray();
                    using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
                    {
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                        return Encoding.UTF8.GetString(decryptedBytes);
                    }
                }
            }
            catch (FormatException) 
            {
                throw new FormatException("Invalid cipher text format.");
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Decryption failed. Data may be corrupted or invalid.", ex);
            }

        }
    }
}
