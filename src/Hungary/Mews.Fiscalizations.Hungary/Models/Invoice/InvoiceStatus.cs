using Mews.Fiscalizations.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class InvoiceStatus
    {
        public InvoiceStatus(InvoiceState status, IEnumerable<InvoiceValidationResult> validationResults, string xmlInvoice = null)
        {
            Status = status;
            ValidationResults = validationResults;
            XmlInvoice = xmlInvoice;
        }

        public InvoiceState Status { get; }
        public string XmlInvoice { get; }
        public IEnumerable<InvoiceValidationResult> ValidationResults { get; }

        internal static Indexed<InvoiceStatus> Map(Dto.ProcessingResultType result)
        {
            return new Indexed<InvoiceStatus>(
                index: result.index,
                value: new InvoiceStatus(
                    status: (InvoiceState)result.invoiceStatus,
                    validationResults: InvoiceValidationResult.Map(result.businessValidationMessages, result.technicalValidationMessages),
                    result.originalRequest.IsNotNull() ? ServiceInfo.Encoding.GetString(result.originalRequest) : null
                )
            );
        }
    }
}
