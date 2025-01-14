﻿using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class Name
    {
        private Name(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<Name, Error> Create(string value)
        {
            return ValidationExtensions.ValidateString(value, minLength: 1, maxLength: 512, regex: ".*[^\\s].*").Map(v => new Name(v));
        }
    }
}
