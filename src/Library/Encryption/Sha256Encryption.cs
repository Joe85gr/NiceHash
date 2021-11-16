using Library.Models;
using System.Security.Cryptography;
using System.Text;

namespace Library.Encryption
{
    public static class Sha256Encryption
    {
        public static string GenerateEncryptedHash(HashStructure hashStructure)
        {
            var text = GenerateText(hashStructure);

            var encryptedHash = GetHash(text, hashStructure.ApiSecret);

            return encryptedHash;
        }

        public static string GetHash(string text, string key)
        {
            var encoding = new UTF8Encoding();

            var textBytes = encoding.GetBytes(text);
            var keyBytes = encoding.GetBytes(key);

            byte[] hashBytes;

            using (var hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        private static string GenerateText(HashStructure hashStructure)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(hashStructure.ApiKey);
            stringBuilder.Append("\x00");
            stringBuilder.Append(hashStructure.Time);
            stringBuilder.Append("\x00");
            stringBuilder.Append(hashStructure.Nonce);
            stringBuilder.Append("\x00");
            stringBuilder.Append("\x00");
            stringBuilder.Append(hashStructure.OrgId);
            stringBuilder.Append("\x00");
            stringBuilder.Append("\x00");
            stringBuilder.Append(hashStructure.Method);
            stringBuilder.Append("\x00");
            stringBuilder.Append(hashStructure.EncodedPath);
            stringBuilder.Append("\x00");
            stringBuilder.Append(hashStructure.Query);

            if (string.IsNullOrEmpty(hashStructure.BodyStr) == false)
            {
                stringBuilder.Append("\x00");
                stringBuilder.Append(hashStructure.BodyStr);
            }

            return stringBuilder.ToString();
        }
    }
}
