﻿using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class EncryptionKey
    {
        private EncryptionKey(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<EncryptionKey, Error> Create(string value)
        {
            return StringValidations.NonEmptyNorWhitespace(value).FlatMap(v =>
            {
                var validEncryptionKey = StringValidations.RegexMatch(v, new Regex("^[0-9A-Za-z]{16}$"));
                return validEncryptionKey.Map(k => new EncryptionKey(k));
            });
        }
    }
}
