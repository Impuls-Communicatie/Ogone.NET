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
            System.Text.Encoding TextEncoding;

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

            switch (encoding)
            {
                case Encoding.UTF8:
                    TextEncoding = new System.Text.UTF8Encoding();
                    break;
                case Encoding.ISO_8859_1:
                    TextEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
                    break;
                default:
                    TextEncoding = new System.Text.UTF8Encoding();
                    break;
            }

            byte[] byteSourceText = TextEncoding.GetBytes(value);
            byte[] byteHash = Algorithm.ComputeHash(byteSourceText);

            string delimitedHexHash = BitConverter.ToString(byteHash);
            string hexHash = delimitedHexHash.Replace("-", "");

            return hexHash;
        }
    }
}
