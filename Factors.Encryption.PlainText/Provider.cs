﻿using Factors.Models.Interfaces;
using System;

namespace Factors.Encryption.PlainText
{
    public class Provider : IFactorsEncryptionProvider
    {
        public string HashData(string text)
        {
            return text;
        }

        public bool VerifyHash(string text, string hash)
        {
            return String.Equals(text, hash, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
