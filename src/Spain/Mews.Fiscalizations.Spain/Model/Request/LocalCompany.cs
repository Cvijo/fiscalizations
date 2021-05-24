﻿using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public sealed class LocalCompany
    {
        public LocalCompany(Name name, TaxpayerIdentificationNumber taxpayerIdentificationNumber)
        {
            Name = Check.IsNotNull(name, nameof(name));
            TaxpayerIdentificationNumber = Check.IsNotNull(taxpayerIdentificationNumber, nameof(taxpayerIdentificationNumber));
        }

        public Name Name { get; }

        public TaxpayerIdentificationNumber TaxpayerIdentificationNumber { get; }
    }
}