using System.Security.Cryptography;
using System.Text;
namespace CipherTool.Service
{
    public class CipherService
    {
        private const string DEFAULT_KEY = "minhhiep";

        public string AesEncrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(DEFAULT_KEY));
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            var result = new byte[aes.IV.Length + cipherBytes.Length];
            aes.IV.CopyTo(result, 0);
            cipherBytes.CopyTo(result, aes.IV.Length);

            return Convert.ToBase64String(result);
        }

        public string AesDecrypt(string cipherText)
        {
            var fullBytes = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(DEFAULT_KEY));

            var iv = fullBytes[..16];
            var cipher = fullBytes[16..];
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            var plainBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
