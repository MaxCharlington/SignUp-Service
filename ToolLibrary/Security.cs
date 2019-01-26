using System.Security.Cryptography;
using System.Text;

namespace ToolLibrary
{
    public class Security
    {
        // Converts a string to a hash
        public static string GetHash(string input)
        {
            byte[] hash;
            using (var alg = new SHA256CryptoServiceProvider())
                hash = alg.ComputeHash(Encoding.Unicode.GetBytes(input));
            var sb = new StringBuilder();
            foreach (byte b in hash) sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

        //Generates random string value
        public static string GetUniqueKey()
        {
            char[] chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[64];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(64);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
