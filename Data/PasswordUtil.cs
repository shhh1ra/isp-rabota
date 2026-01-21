using System;
using System.Security.Cryptography;
using System.Text;

namespace AutoSalon.Desktop.Data
{
    public static class PasswordUtil
    {
        public static string Hash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }
}
