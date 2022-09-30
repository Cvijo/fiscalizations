using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models
{

    public sealed class ItemAmounts
    {
        public ItemAmounts(Amount amount, Amount amountHUF, decimal taxRatePercentage)
        {
            Amount = Check.IsNotNull(amount, nameof(amount));
            AmountHUF = Check.IsNotNull(amountHUF, nameof(amountHUF));
            TaxRate = new TaxRate(taxRatePercentage);
        }
        public ItemAmounts(Amount amount, Amount amountHUF, TaxRate taxRate)
        {
            Amount = Check.IsNotNull(amount, nameof(amount));
            AmountHUF = Check.IsNotNull(amountHUF, nameof(amountHUF));
            TaxRate = taxRate;
        }

        public Amount Amount { get; }

        public Amount AmountHUF { get; }

        public TaxRate TaxRate { get; }
    }

    public sealed class ItemAmounts_old
    {
        public ItemAmounts_old(Amount amount, Amount amountHUF, decimal? taxRatePercentage = null)
        {
            Amount = Check.IsNotNull(amount, nameof(amount));
            AmountHUF = Check.IsNotNull(amountHUF, nameof(amountHUF));
            TaxRatePercentage = taxRatePercentage.ToOption();

            if (taxRatePercentage.HasValue)
            {
                Check.In(taxRatePercentage.Value, TaxationInfo.PercentageTaxRates, nameof(taxRatePercentage));
            }
        }

        public Amount Amount { get; }

        public Amount AmountHUF { get; }

        public IOption<decimal> TaxRatePercentage { get; }
    }

}
