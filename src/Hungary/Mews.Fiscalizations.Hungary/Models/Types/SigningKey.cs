﻿using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class SigningKey
    {
        private SigningKey(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<SigningKey, Error> Create(string value)
        {
            return StringValidations.NonEmptyNorWhitespace(value).FlatMap(v =>
            {
                var validSigningKey = StringValidations.RegexMatch(v, new Regex("^[0-9A-Za-z]{2}[-]{1}[0-9A-Za-z]{4}[-]{1}[0-9A-Za-z]{24}$"));
                return validSigningKey.Map(k => new SigningKey(k));
            });
        }
    }
}
