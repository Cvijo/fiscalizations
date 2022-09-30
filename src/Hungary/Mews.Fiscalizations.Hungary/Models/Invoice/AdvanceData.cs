using Mews.Fiscalizations.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class AdvanceData
    {
        /// <summary>
        /// Indicate that this item is advance payment.
        /// </summary>
        /// <param name="advanceIndicator"></param>
        public AdvanceData()
        {
            AdvanceIndicator = true;
        }

        /// <summary>
        /// When using advance payment as negative item on final invoice, specify data from original advance invoice
        /// </summary>
        /// <param name="advanceOriginalInvoice"></param>
        /// <param name="advancePaymentDate"></param>
        /// <param name="advanceExchangeRate"></param>
        public AdvanceData(InvoiceNumber advanceOriginalInvoice, DateTime advancePaymentDate, ExchangeRate advanceExchangeRate)
        {
            AdvanceIndicator = true;
            AdvanceOriginalInvoice = Check.IsNotNull(advanceOriginalInvoice, nameof(advanceOriginalInvoice));
            AdvancePaymentDate = Check.IsNotNull(advancePaymentDate, nameof(advancePaymentDate));
            AdvanceExchangeRate = Check.IsNotNull(advanceExchangeRate, nameof(advanceExchangeRate));
        }
        public bool AdvanceIndicator { get; }
        public InvoiceNumber AdvanceOriginalInvoice { get; }
        public DateTime AdvancePaymentDate { get; }
        public ExchangeRate AdvanceExchangeRate { get; }
    }
}
