using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class TaxRate
    {
        public TaxRate(decimal taxRatePercentage)
        {
            TaxRatePercentage = taxRatePercentage.ToOption();
            TaxRateType = TaxRateTypes.VatPercentage;
            Check.In(taxRatePercentage, TaxationInfo.PercentageTaxRates, nameof(taxRatePercentage));
        }

        public TaxRate(TaxRateTypes taxRateType, string  @case, string reason)
        {
            TaxRatePercentage = Option.Empty<decimal>();
            TaxRateType = taxRateType;
            Case = @case;
            Reason = reason;
            if( TaxRateType == TaxRateTypes.VatExemption)
            {
                Check.In(Case, TaxationInfo.VatExemptCases, $"Vat exempt tax case not valid. Available values:{ string.Join(",", TaxationInfo.VatExemptCases)}");
                Check.Condition(!Reason.IsNullOrWhitespace(), $"For enter reason for {TaxRateType}");
            }
            if (TaxRateType == TaxRateTypes.VatOutOfScope)
            {
                Check.In(Case, TaxationInfo.VatOutOfScopeCases, $"Vat out of scope case not valid. Available values:{ string.Join(",", TaxationInfo.VatOutOfScopeCases)}");
                Check.Condition(!Reason.IsNullOrWhitespace(), $"For enter reason for {TaxRateType}");
            }
        }

        public IOption<decimal> TaxRatePercentage { get; }
        public TaxRateTypes TaxRateType { get; }

        public string Case { get; }
        public string Reason { get; }

    }
}
