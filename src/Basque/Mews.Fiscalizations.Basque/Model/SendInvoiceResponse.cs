﻿using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System;
using System.Collections.Generic;

namespace Mews.Fiscalizations.Basque.Model
{
    public sealed class SendInvoiceResponse
    {
        public SendInvoiceResponse(
            string xmlRequestContent,
            string xmlResponseContent,
            string qrCodeUri,
            string tbaiIdentifier,
            DateTime received,
            InvoiceState state,
            string description,
            string stateExplanation,
            String1To100 signatureValue,
            string csv,
            IEnumerable<SendInvoiceValidationResult> validationResults = null)
        {
            XmlRequestContent = xmlRequestContent;
            XmlResponseContent = xmlResponseContent;
            QrCodeUri = qrCodeUri;
            TBAIIdentifier = tbaiIdentifier;
            Received = received;
            State = state;
            Description = description;
            StateExplanation = stateExplanation;
            SignatureValue = signatureValue;
            CSV = csv.ToNonEmptyOption();
            ValidationResults = validationResults.ToOption();
        }

        public string XmlRequestContent { get; }

        public string XmlResponseContent { get; }

        public string QrCodeUri { get; }

        public string TBAIIdentifier { get; }

        public DateTime Received { get; }

        public InvoiceState State { get; }

        public string Description { get; }

        public string StateExplanation { get; }

        public String1To100 SignatureValue { get; }

        public IOption<string> CSV { get; }

        public IOption<IEnumerable<SendInvoiceValidationResult>> ValidationResults { get; }
    }
}