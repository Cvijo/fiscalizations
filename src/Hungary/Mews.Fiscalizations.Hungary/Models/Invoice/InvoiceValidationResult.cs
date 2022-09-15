using Mews.Fiscalizations.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class InvoiceValidationResult
    {
        public InvoiceValidationResult(string message, ValidationResultCode resultCode, string additionalInfo)
        {
            Message = message;
            ResultCode = resultCode;
            AdditionalInfo = additionalInfo;
        }

        public string Message { get; }
        public string AdditionalInfo { get; }
        public ValidationResultCode ResultCode { get; }

        internal static IEnumerable<InvoiceValidationResult> Map(
            IEnumerable<Dto.BusinessValidationResultType> businessValidations,
            IEnumerable<Dto.TechnicalValidationResultType> technicalValidations)
        {
            return Enumerable.Concat(
                businessValidations.OrEmptyIfNull().Select(v => new InvoiceValidationResult(
                    message: v.message,
                    additionalInfo: $"Original invoice: {v.pointer.originalInvoiceNumber} | Tag:{v.pointer.tag} | Line: {v.pointer.line}{Environment.NewLine} Value:{v.pointer.value}",
                    resultCode: (ValidationResultCode)v.validationResultCode
                )),
                technicalValidations.OrEmptyIfNull().Select(v => new InvoiceValidationResult(
                    message: v.message,
                    additionalInfo: v.validationErrorCode,
                    resultCode: (ValidationResultCode)v.validationResultCode
                ))
            );
        }
    }
}
