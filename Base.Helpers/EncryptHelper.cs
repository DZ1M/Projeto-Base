using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Base.Helpers
{
    public static class EncryptHelper
    {

        public static string Sha1(this string valor)
        {
            using var sha1 = SHA1.Create();
            return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(valor))).Replace("-", "").ToLower();
        }

        public static string Md5(this string valor)
        {
            using (var md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(valor))).Replace("-", "").ToLower();
            }
        }
        public const string salt = "sdg23fdsg65df4b4kjkj465kg487rt89";
        public static string EncryptRijndael(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            var aesAlg = NewRijndaelManaged(salt);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(text);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }
        private static RijndaelManaged NewRijndaelManaged(string salt)
        {
            if (salt == null) throw new ArgumentNullException("salt");
            var saltBytes = Encoding.ASCII.GetBytes(salt);
            var key = new Rfc2898DeriveBytes("560A18CD-6346-4CF0-A2E8-671F9B6B9EA9", saltBytes);

            var aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

            return aesAlg;
        }
        public static string DecryptRijndael(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("cipherText");

            if (!IsBase64String(cipherText))
                throw new Exception("The cipherText input parameter is not base64 encoded");

            string text;

            var aesAlg = NewRijndaelManaged(salt);
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            var cipher = Convert.FromBase64String(cipherText);

            using (var msDecrypt = new MemoryStream(cipher))
            {
                var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                var srDecrypt = new StreamReader(csDecrypt);
                text = srDecrypt.ReadToEnd();
            }
            return text;
        }
        public static bool IsBase64String(string base64String)
        {
            base64String = base64String.Trim();
            return (base64String.Length % 4 == 0) &&
                   Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }

    }
}
