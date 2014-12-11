using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conferences.Utils;

namespace System
{
    public static class EncryptionExtensions
    {
        public static string Encrypt(this string value)
        {
            return Encryption.Encrypt(value);
        }

        public static string Decrypt(this string value)
        {
            return Encryption.Decrypt(value);
        }
    }
}
