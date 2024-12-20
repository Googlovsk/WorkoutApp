using System.Security.Cryptography;
using System.Text;

namespace Schedule.Data.Enc
{
    internal class Enc
    {
        //Генерация соли хэша, честно украдена с Гитхаба
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        //Генерация хэша, честно украдено с Гитхаба
        public static string HashPassword(string password, string salt)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                var saltedPassword = password + salt;
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
