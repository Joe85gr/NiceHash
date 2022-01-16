using System;
using System.Security.Cryptography;
using System.Text;

namespace Server.Encryption
{
    public static class Sha256Encryption
    {
        public static string GenerateHash(string text, string key)
        {
            var encoding = new UTF8Encoding();

            var textBytes = encoding.GetBytes(text);
            var keyBytes = encoding.GetBytes(key);

            byte[] hashBytes;

            using (var hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        public static string GenerateTextToHash(HashStructure hashStructure)
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
