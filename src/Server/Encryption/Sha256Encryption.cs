using System;
using System.Security.Cryptography;
using System.Text;
using Library.Models;

namespace Server.Encryption
{
    public static class Sha256Encryption
    {
        public static string GenerateEncryptedHash(HashStructure hashStructure)
        {
            var text = GenerateText(hashStructure);

            var encryptedHash = GetHash(text, hashStructure.ApiSecret);

            return encryptedHash;
        }

        private static string GetHash(string text, string key)
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
            stringBuilder.Append('\0');
            stringBuilder.Append(hashStructure.Time);
            stringBuilder.Append('\0');
            stringBuilder.Append(hashStructure.Nonce);
            stringBuilder.Append('\0');
            stringBuilder.Append('\0');
            stringBuilder.Append(hashStructure.OrgId);
            stringBuilder.Append('\0');
            stringBuilder.Append('\0');
            stringBuilder.Append(hashStructure.Method);
            stringBuilder.Append('\0');
            stringBuilder.Append(hashStructure.EncodedPath);
            stringBuilder.Append('\0');
            stringBuilder.Append(hashStructure.Query);

            if (string.IsNullOrEmpty(hashStructure.BodyStr)) return stringBuilder.ToString();
            
            stringBuilder.Append('\0');
            stringBuilder.Append(hashStructure.BodyStr);

            return stringBuilder.ToString();
        }
    }
}
