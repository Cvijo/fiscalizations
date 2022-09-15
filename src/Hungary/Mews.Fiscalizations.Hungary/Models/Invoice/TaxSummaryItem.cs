using FuncSharp;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class TaxSummaryItem
    {
        public TaxSummaryItem(Amount amount, Amount amountHUF, TaxRate taxRate)
        {
            Amount = amount;
            AmountHUF = amountHUF;
            TaxRate = taxRate;
            //TaxRatePercentage = taxRatePercentage.ToOption();
            
        }

        public Amount Amount { get; }

        public Amount AmountHUF { get; }

        public TaxRate TaxRate { get; }
        //public IOption<decimal> TaxRatePercentage { get; }
    }
}
