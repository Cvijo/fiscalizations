﻿using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Hungary.Models;
using Mews.Fiscalizations.Hungary.Utils;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Hungary
{
    public sealed class NavClient
    {
        private TechnicalUser TechnicalUser { get; }

        private SoftwareIdentification SoftwareIdentification { get; }

        private static HttpClient HttpClient { get; }

        private Client Client { get; }

        static NavClient()
        {
            HttpClient = new HttpClient();
        }

        public NavClient(TechnicalUser technicalUser, SoftwareIdentification softwareIdentification, NavEnvironment environment)
        {
            TechnicalUser = technicalUser;
            SoftwareIdentification = softwareIdentification;
            Client = new Client(HttpClient, environment);
        }

        public Task<ResponseResult<ExchangeToken, ExchangeTokenErrorCode>> GetExchangeTokenAsync()
        {
            var request = RequestCreator.CreateTokenExchangeRequest(TechnicalUser, SoftwareIdentification);
            return Client.ProcessRequestAsync<Dto.TokenExchangeRequest, Dto.TokenExchangeResponse, ExchangeToken, ExchangeTokenErrorCode>(
                endpoint: "tokenExchange",
                request: request,
                successFunc: (responseDto, requestXml, responseXml) => ModelMapper.MapExchangeToken(requestXml, responseXml, responseDto, TechnicalUser)
            );
        }

        public Task<ResponseResult<TransactionStatus, TransactionErrorCode>> GetTransactionStatusAsync(string transactionId, bool returnOriginalXml = false)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                throw new ArgumentException($"{nameof(transactionId)} must be specified.");
            }

            var request = RequestCreator.CreateQueryTransactionStatusRequest(TechnicalUser, SoftwareIdentification, transactionId, returnOriginalXml);
            return Client.ProcessRequestAsync<Dto.QueryTransactionStatusRequest, Dto.QueryTransactionStatusResponse, TransactionStatus, TransactionErrorCode>(
                endpoint: "queryTransactionStatus",
                request: request,
                successFunc: (responseDto, requestXml, responseXml) => ModelMapper.MapTransactionStatus(requestXml, responseXml, responseDto)
            );
        }

        
        public Task<ResponseResult<InvoiceData, ResultErrorCode>> GetInvoiceDataAsync(string invoiceNumber)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
            {
                throw new ArgumentException($"{nameof(invoiceNumber)} must be specified.");
            }

            var request = RequestCreator.CreateQueryInvoiceDataRequest(TechnicalUser, SoftwareIdentification, invoiceNumber);
            return Client.ProcessRequestAsync<Dto.QueryInvoiceDataRequest, Dto.QueryInvoiceDataResponse, InvoiceData, ResultErrorCode>(
                endpoint: "queryInvoiceData",
                request: request,
                successFunc: (responseDto, requestXml, responseXml) => ModelMapper.MapInvoiceDataResponse(requestXml, responseXml, responseDto)
            );
        }

        public Task<ResponseResult<TaxPayerData, TaxPayerErrorCode>> GetTaxPayerDataAsync(TaxpayerIdentificationNumber taxId)
        {
            var request = RequestCreator.CreateQueryTaxpayerRequest(TechnicalUser, SoftwareIdentification, taxId.TaxpayerNumber);
            return Client.ProcessRequestAsync<Dto.QueryTaxpayerRequest, Dto.QueryTaxpayerResponse, TaxPayerData, TaxPayerErrorCode>(
                endpoint: "queryTaxpayer",
                request: request,
                successFunc: (responseDto, requestXml, responseXml) => ModelMapper.MapTaxPayerData(requestXml, responseXml, responseDto)
            );
        }

        public Task<ResponseResult<string, ResultErrorCode>> SendInvoicesAsync(ExchangeToken token, ISequence<Invoice> invoices)
        {
            var request = RequestCreator.CreateManageInvoicesRequest(TechnicalUser, SoftwareIdentification, token, invoices);
            return ManageInvoicesAsync(request, invoices);
        }

        public Task<ResponseResult<string, ResultErrorCode>> SendModificationDocumentsAsync(ExchangeToken token, ISequence<ModificationInvoice> invoices)
        {
            var request = RequestCreator.CreateManageInvoicesRequest(TechnicalUser, SoftwareIdentification, token, invoices);
            return ManageInvoicesAsync(request, invoices);
        }

        public Task<ResponseResult<string, ResultErrorCode>> SendAnnulmentAsync(ExchangeToken token, InvoiceNumber invoiceNumber, string reason, AnnulmentCode annulmentCode = AnnulmentCode.ERRATIC_DATA)
        {

            if (string.IsNullOrEmpty(reason))
            {
                throw new ArgumentException($"{nameof(reason)} must be specified.");
            }

            var request = RequestCreator.CreateInvoiceAnnulmentRequest(TechnicalUser, SoftwareIdentification, token, invoiceNumber, annulmentCode, reason);

            return Client.ProcessRequestAsync<Dto.ManageAnnulmentRequest, Dto.ManageAnnulmentResponse, string, ResultErrorCode>(
                endpoint: "manageAnnulment",
                request: request,
                successFunc: (responseDto, requestXml, responseXml) => ModelMapper.MapInvoiceAnnulment(requestXml, responseXml, responseDto)
            );
        }

        private Task<ResponseResult<string, ResultErrorCode>> ManageInvoicesAsync<TDocument>(Dto.ManageInvoiceRequest request, ISequence<TDocument> invoices)
        {
            if (invoices.Values.Count() > ServiceInfo.MaxInvoiceBatchSize)
            {
                throw new ArgumentException($"Max invoice batch size ({ServiceInfo.MaxInvoiceBatchSize}) exceeded.", nameof(invoices));
            }

            if (invoices.StartIndex != 1)
            {
                throw new ArgumentException("Items need to be indexed from 1.", nameof(invoices));
            }

            return Client.ProcessRequestAsync<Dto.ManageInvoiceRequest, Dto.ManageInvoiceResponse, string, ResultErrorCode>(
                endpoint: "manageInvoice",
                request: request,
                successFunc: (responseDto, requestXml, responseXml) => ModelMapper.MapManageInvoice(requestXml, responseXml, responseDto)
            );
        }
    }
}