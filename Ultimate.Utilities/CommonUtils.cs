using System;
using System.Security.Cryptography;
using System.Text;

namespace Ultimate.Utilities
{
    /// <summary>
    /// Common Utility methods.
    /// </summary>
    public static class CommonUtils
    {
        /// <summary>
        /// Encrypts the string using MD5 encryption and returns bytes
        /// </summary>
        /// <param name="input">string to encrypt</param>
        /// <returns>byte array</returns>
        public static byte[] GetMd5HashBytes(string input)
        {
            var hash = MD5.Create();
            return hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Encrypts the string using MD5 encryption and returns string
        /// </summary>
        /// <param name="input">string to encrypt</param>
        /// <param name="returnBase64">return base64 string? (default false)</param>
        /// <returns>encrypted string</returns>
        public static string GetMd5HashString(string input,bool returnBase64 = false)
        {
            var hash = MD5.Create();
            var bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            return returnBase64 ? Convert.ToBase64String(bytes) : Convert.ToString(bytes);

        }


    }
}
