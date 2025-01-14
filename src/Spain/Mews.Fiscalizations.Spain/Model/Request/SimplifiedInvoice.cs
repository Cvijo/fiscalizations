﻿using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public class SimplifiedInvoice
    {
        public SimplifiedInvoice(
            TaxPeriod taxPeriod,
            InvoiceId id,
            TaxMode taxMode,
            String0To500 description,
            TaxBreakdown taxBreakdown,
            bool issuedByThirdParty)
        {
            TaxPeriod = Check.IsNotNull(taxPeriod, nameof(taxPeriod));
            Id = Check.IsNotNull(id, nameof(id));
            TaxMode = taxMode;
            Description = Check.IsNotNull(description, nameof(description));
            TaxBreakdown = Check.IsNotNull(taxBreakdown, nameof(taxBreakdown));
            IssuedByThirdParty = issuedByThirdParty;
        }

        public TaxPeriod TaxPeriod { get; }

        public InvoiceId Id { get; }

        public TaxMode TaxMode { get; }

        public String0To500 Description { get; }

        public TaxBreakdown TaxBreakdown { get; }

        public bool IssuedByThirdParty { get; }

        public decimal TotalAmount { get { return TaxBreakdown.TotalAmount; } }
    }
}
