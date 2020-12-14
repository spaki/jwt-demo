using System.Security.Cryptography;
using System.Text;

namespace JWTDemo.Infra
{
    public static class Cryptography
    {
        public static string ToMD5(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var result = new StringBuilder();
            var bytes = new MD5CryptoServiceProvider().ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString("X2"));

            return result.ToString();
        }
    }
}
