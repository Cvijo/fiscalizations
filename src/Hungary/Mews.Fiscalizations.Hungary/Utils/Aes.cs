﻿using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Mews.Fiscalizations.Hungary.Utils
{
    internal static class Aes
    {
        public static byte[] Decrypt(string key, byte[] data)
        {
            var cipher = CipherUtilities.GetCipher("AES");
            cipher.Init(false, new KeyParameter(ServiceInfo.Encoding.GetBytes(key)));
            return cipher.DoFinal(data);
        }
    }
}