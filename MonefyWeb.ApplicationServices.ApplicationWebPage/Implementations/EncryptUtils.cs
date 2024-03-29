﻿using System.Security.Cryptography;
using System.Text;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;

namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Implementations
{
    public class EncryptUtils : IEncryptUtils
    {
        public string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
