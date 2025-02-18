﻿using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class Login
    {
        private Login(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<Login, Error> Create(string value)
        {
            return ValidationExtensions.ValidateString(value, minLength: 1, maxLength: 15, regex: "^[0-9A-Za-z]{15}$").Map(v => new Login(v));
        }
    }
}
