﻿using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Spain.Model.Request
{
    public sealed class TaxRateSummary
    {
        public TaxRateSummary(Percentage taxRatePercentage, Amount taxBaseAmount, Amount taxAmount)
        {
            TaxRatePercentage = Check.IsNotNull(taxRatePercentage, nameof(taxRatePercentage));
            TaxBaseAmount = Check.IsNotNull(taxBaseAmount, nameof(taxBaseAmount));
            TaxAmount = Check.IsNotNull(taxAmount, nameof(taxAmount));
        }

        public Percentage TaxRatePercentage { get; }

        public Amount TaxBaseAmount { get; }

        public Amount TaxAmount { get; }
    }
}
