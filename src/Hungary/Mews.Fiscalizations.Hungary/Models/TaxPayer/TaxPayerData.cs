using FuncSharp;
using System;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class TaxPayerData
    {
        public TaxPayerData(string id, string name, string shortName, Address address, string vatCode, IncorporationType incorporationType, DateTime? infoDate = null)
        {
            Id = id;
            Name = name;
            ShortName = shortName;
            Address = address;
            VatCode = vatCode;
            IncorporationType = incorporationType;
            InfoDate = infoDate.ToOption();
        }

        public string Id { get; }

        public string Name { get; }

        public string ShortName { get; }

        public Address Address { get; }

        public string VatCode { get; }

        public IncorporationType IncorporationType { get; }

        public IOption<DateTime> InfoDate { get; }
    }
}
