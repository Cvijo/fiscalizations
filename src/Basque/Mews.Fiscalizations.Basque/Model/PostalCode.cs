﻿using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Basque.Model
{
    public sealed class PostalCode
    {
        private PostalCode(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<PostalCode, Error> Create(string value)
        {
            return StringValidations.RegexMatch(value, new Regex("^[a-zA-Z0-9]{1,20}$")).Map(v => new PostalCode(v));
        }

        public static PostalCode CreateUnsafe(string value)
        {
            return Create(value).GetUnsafe();
        }
    }
}