using Mews.Fiscalizations.Core.Model;
using System;
using System.Collections.Generic;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class ModificationInvoice : Invoice
    {
        public InvoiceNumber OriginalDocumentNumber { get; }

        /// <summary>
        /// Sequential index of the modification document for one original document.
        /// </summary>
        public int ModificationIndex { get; }

        /// <summary>
        /// Number of items already reported in the original document + all preceding modification documents.
        /// </summary>
        public int ItemIndexOffset { get; }

        /// <summary>
        /// Indication of a modification for a base invoice with no data reporting completed at the time of the modification (no original invoice).
        /// </summary>
        public bool ModifyWithoutMaster { get; }

        public ModificationInvoice(
            InvoiceNumber number,
            int modificationIndex,
            int itemIndexOffset,
            InvoiceNumber originalDocumentNumber,
            DateTime issueDate,
            DateTime paymentDate,
            SupplierInfo supplierInfo,
            Receiver receiver,
            CurrencyCode currencyCode,
            ISequence<InvoiceItem> items,
            bool isSelfBilling = false,
            bool isCashAccounting = false,
            bool modifyWithoutMaster = false,
            DateTime? deliveryDate = null,
            PaymentMethod? paymentMethod = null,            
            InvoiceCategory invoiceCategory = Models.InvoiceCategory.NORMAL,
            InvoiceAppearance invoiceAppearance = Models.InvoiceAppearance.Electric,
            List<AdditionalInvoiceData> additionalInvoiceData = null)
            : base(number, issueDate, paymentDate, supplierInfo, receiver, currencyCode, items, isSelfBilling, isCashAccounting, deliveryDate, paymentMethod, invoiceCategory, invoiceAppearance, additionalInvoiceData)
        {
            OriginalDocumentNumber = originalDocumentNumber;
            ModificationIndex = modificationIndex;
            ItemIndexOffset = itemIndexOffset;
            ModifyWithoutMaster = modifyWithoutMaster;
        }
    }
}