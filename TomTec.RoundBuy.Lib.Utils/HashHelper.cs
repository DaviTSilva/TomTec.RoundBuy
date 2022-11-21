using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TomTec.RoundBuy.Lib.Utils
{
    public class HashHelper
    {
        static public string GenerateSalt()
        {
            int max_length = 22;
            var salt = new byte[max_length];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return "$2a$10$" + Convert.ToBase64String(salt);
        }
    }
}
