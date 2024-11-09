using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Hash
{
    public static class HashHelper
    {
        public static string HashItem(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Verilen girdiyi byte dizisine dönüştür
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // byte dizisini hex stringine dönüştür
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GenerateHash(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] combinedBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        // Rastgele salt oluşturma metodu
        public static string GenerateSalt(int size = 16)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[size];
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }

        // Şifre doğrulama metodu
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            // Kullanıcıdan gelen şifreyi ve veritabanındaki salt değerini kullanarak hash oluştur
            string newHash = GenerateHash(enteredPassword, storedSalt);

            // Yeni hash ile saklanan hash'i karşılaştır
            return newHash == storedHash;
        }
    }
}
