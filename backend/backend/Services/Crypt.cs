using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace backend.Services
{
    public static class Crypt
    {
        public static string cryptKey { get; set; } = "DmitriiTalentDmitriiTale";

        public static string Hash(string password)
        {
            using (var sha = SHA256.Create())
            {
                var hashedPasswordBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();
                return hashedPassword;
            }
        }
        public static string Encrypt(string plaintext, string key = "DmitriiTalentDmitriiTale")
        {
            using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider())
            {
                byte[] keys = Encoding.UTF8.GetBytes(key);
                ICryptoTransform encryptor = provider.CreateEncryptor(keys, keys);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                byte[] input = Encoding.UTF8.GetBytes(plaintext);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static string Decrypt(string CipherText, string key = "DmitriiTalentDmitriiTale")
        {
            CipherText = HttpUtility.UrlDecode(CipherText);
            CipherText = CipherText.Replace(" ", "+");
            byte[] buffer = Convert.FromBase64String(CipherText);
            using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider())
            {
                byte[] keys = Encoding.UTF8.GetBytes(key);
                ICryptoTransform encryptor = provider.CreateDecryptor(keys, keys);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                cs.Write(buffer, 0, buffer.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
