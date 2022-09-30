using FuncSharp;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class ItemDiscount
    {
        /// <summary>
        /// Set total discount on line item in invoice currency
        /// </summary>
        /// <param name="discountNetTotal"></param>
        /// <param name="discountPercentage"></param>
        /// <param name="discountDescription"></param>
        public ItemDiscount(decimal? discountNetTotal = null, decimal? discountPercentage = null, string discountDescription = null)
        {
            DiscountNetTotal = discountNetTotal.ToOption();
            DiscountPercentage = discountPercentage.ToOption();            
            DiscountDescription = discountDescription;
        }

        public IOption<decimal> DiscountNetTotal { get; }
        public IOption<decimal> DiscountPercentage { get; }
        public string DiscountDescription { get; }

    }
}
