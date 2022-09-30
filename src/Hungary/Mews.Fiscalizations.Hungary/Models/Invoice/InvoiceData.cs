using FuncSharp;
using System;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class InvoiceData
    {
        public InvoiceData(string transaciontId, DateTime uploadDate, string upladUser, string source, string xmlInvoice)
        {
            TransactionId = transaciontId;
            UploadDate = uploadDate;
            UploadUser = upladUser;
            Source = source;
            XmlInvoice = xmlInvoice;
        }


        public string TransactionId { get; }
        public DateTime UploadDate { get; }
        public string UploadUser { get; }
        public string Source { get; }
        public string XmlInvoice { get; }

    }
}
