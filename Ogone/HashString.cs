using System;
using System.Security.Cryptography;
using System.Text;

namespace Ogone
{
    class HashString
    {
        public static string GenerateHash(string value, Encoding encoding, SHA hashAlgorithm)
        {
            HashAlgorithm Algorithm;

            switch (hashAlgorithm)
            {
                case SHA.SHA1:
                    Algorithm = new SHA1CryptoServiceProvider();
                    break;
                case SHA.SHA256:
                    Algorithm = new SHA256CryptoServiceProvider();
                    break;
                case SHA.SHA512:
                    Algorithm = new SHA512CryptoServiceProvider();
                    break;
                default:
                    Algorithm = new SHA1CryptoServiceProvider();
                    break;
            }

            byte[] byteSourceText = encoding.GetBytes(value);
            byte[] byteHash = Algorithm.ComputeHash(byteSourceText);

            string delimitedHexHash = BitConverter.ToString(byteHash);
            string hexHash = delimitedHexHash.Replace("-", "");

            return hexHash;
        }
    }
}
