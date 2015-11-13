using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ogone
{
    class HashString
    {
        public static string GenerateHash(string value, Encoding encoding, HashAlgorithm hashAlgorithm)
        {
            byte[] byteSourceText = encoding.GetBytes(value);
            byte[] byteHash = hashAlgorithm.ComputeHash(byteSourceText);
            string result = "";
            foreach (byte b in byteHash)
            {
                result += b.ToString("x2");
            }
            return result;
        }
    }
}
