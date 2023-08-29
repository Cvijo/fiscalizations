using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Hungary.Dto;
using Mews.Fiscalizations.Hungary.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalizations.Hungary
{
    internal static class RequestMapper
    {
        internal static Dto.InvoiceData MapModificationInvoice(ModificationInvoice invoice)
        {
            var lines = MapItems(invoice, invoice.ItemIndexOffset);
            var invoiceReference = new Dto.InvoiceReferenceType
            {
                modificationIndex = invoice.ModificationIndex,
                originalInvoiceNumber = invoice.OriginalDocumentNumber.Value,
                modifyWithoutMaster = invoice.ModifyWithoutMaster
            };

            var invoiceDto = GetCommonInvoice(invoice, lines, invoiceReference);
            return GetCommonInvoiceData(invoice, invoiceDto);
        }

        internal static Dto.InvoiceData MapInvoice(Invoice invoice)
        {
            var lines = MapItems(invoice);
            var invoiceDto = GetCommonInvoice(invoice, lines);
            return GetCommonInvoiceData(invoice, invoiceDto);
        }

        private static Dto.InvoiceData GetCommonInvoiceData(Invoice invoice, Dto.InvoiceType invoiceDto)
        {

            return new Dto.InvoiceData
            {
                invoiceIssueDate = invoice.IssueDate,
                invoiceNumber = invoice.Number.Value,
                completenessIndicator = invoice.Receiver.Match(
                    customer => false,
                    company => (invoice.InvoiceAppearance.Get() != InvoiceAppearance.Paper) //is it is paper apperiance must be false even if company is Reciver
                ),
                invoiceMain = new Dto.InvoiceMainType
                {
                    Items = new object[] { invoiceDto }
                }
            };
        }

        private static Dto.InvoiceType GetCommonInvoice(Invoice invoice, IEnumerable<Dto.LineType> lines, Dto.InvoiceReferenceType invoiceReference = null)
        {
            var invoiceAmount = Models.Amount.Sum(invoice.Items.Values.Select(i => i.Value.TotalAmounts.Amount));
            var invoiceAmountHUF = Models.Amount.Sum(invoice.Items.Values.Select(i => i.Value.TotalAmounts.AmountHUF));
            var supplierInfo = invoice.SupplierInfo;
            var receiver = invoice.Receiver;
            return new Dto.InvoiceType
            {
                invoiceReference = invoiceReference,
                invoiceLines = new Dto.LinesType
                {
                    line = lines.ToArray()
                },
                invoiceHead = new Dto.InvoiceHeadType
                {
                    invoiceDetail = new Dto.InvoiceDetailType
                    {
                        exchangeRate = invoice.ExchangeRate.Value,
                        currencyCode = invoice.CurrencyCode.Value,
                        invoiceAppearance = invoice.InvoiceAppearance.Map(m => MapInvoiceAppearance(m)).GetOrElse(Dto.InvoiceAppearanceType.ELECTRONIC),
                        invoiceCategory = invoice.InvoiceCategory.Map(m => MapInvoiceCategory(m)).GetOrElse(Dto.InvoiceCategoryType.NORMAL),
                        invoiceDeliveryDate = invoice.DeliveryDate,
                        paymentDate = invoice.PaymentDate,
                        paymentDateSpecified = true,
                        selfBillingIndicator = invoice.IsSelfBilling,
                        cashAccountingIndicator = invoice.IsCashAccounting,
                        paymentMethod = invoice.PaymentMethod.Map(m => MapPaymentMethod(m)).GetOrElse(Dto.PaymentMethodType.OTHER),
                        paymentMethodSpecified = invoice.PaymentMethod.NonEmpty,
                        additionalInvoiceData = GetAdditionalInvoiceData(invoice),
                    },
                    supplierInfo = new Dto.SupplierInfoType
                    {
                        supplierName = supplierInfo.Name.Value,
                        supplierAddress = MapAddress(supplierInfo.Address),
                        supplierTaxNumber = new Dto.TaxNumberType
                        {
                            taxpayerId = supplierInfo.TaxpayerId.Value.TaxpayerNumber,
                            vatCode = supplierInfo.VatCode.Value
                        }
                    },
                    customerInfo = new Dto.CustomerInfoType
                    {
                        customerName = receiver.Match(
                            customer => null,
                            company => company.Name.Value
                        ),
                        customerAddress = receiver.Match(
                            customer => null,
                            company => MapAddress(company.Address)
                        ),
                        customerVatStatus = receiver.Match(
                            customer => Dto.CustomerVatStatusType.PRIVATE_PERSON,
                            company => company.Match(
                                local => Dto.CustomerVatStatusType.DOMESTIC,
                                foreign => Dto.CustomerVatStatusType.OTHER
                            )
                        ),
                        customerVatData = receiver.Match(
                            customer => Option.Empty<Dto.CustomerVatDataType>(),
                            company => company.Match(
                                local => GetCustomerVatDataType(local.TaxpayerId.Value).ToOption(),
                                foreign => foreign.TaxpayerId.Map(i => GetCustomerVatDataType(i))
                            )
                        ).GetOrNull()
                    },
                },
                invoiceSummary = new Dto.SummaryType
                {
                    summaryGrossData = new Dto.SummaryGrossDataType
                    {
                        invoiceGrossAmount = invoiceAmount.Gross.Value,
                        invoiceGrossAmountHUF = invoiceAmountHUF.Gross.Value
                    },
                    Items = new object[]
                    {
                        MapTaxSummary(invoice, invoiceAmount, invoiceAmountHUF)
                    }
                }
            };

        }

        private static Dto.PaymentMethodType MapPaymentMethod(PaymentMethod paymentMethod)
        {
            return paymentMethod.Match(
                PaymentMethod.Card, _ => Dto.PaymentMethodType.CARD,
                PaymentMethod.Cash, _ => Dto.PaymentMethodType.CASH,
                PaymentMethod.Transfer, _ => Dto.PaymentMethodType.TRANSFER,
                PaymentMethod.Voucher, _ => Dto.PaymentMethodType.VOUCHER,
                PaymentMethod.Other, _ => Dto.PaymentMethodType.OTHER
            );
        }

        private static Dto.InvoiceCategoryType MapInvoiceCategory(InvoiceCategory invoiceCategory)
        {
            return invoiceCategory.Match(
                InvoiceCategory.NORMAL, _ => Dto.InvoiceCategoryType.NORMAL,
                InvoiceCategory.AGGREGATE, _ => Dto.InvoiceCategoryType.AGGREGATE,
                InvoiceCategory.SIMPLIFIED, _ => Dto.InvoiceCategoryType.SIMPLIFIED
            );
        }

        private static Dto.InvoiceAppearanceType MapInvoiceAppearance(InvoiceAppearance invoiceAppearance)
        {
            return invoiceAppearance.Match(
                InvoiceAppearance.Electric, _ => Dto.InvoiceAppearanceType.ELECTRONIC,
                InvoiceAppearance.EDI, _ => Dto.InvoiceAppearanceType.EDI,
                InvoiceAppearance.Paper, _ => Dto.InvoiceAppearanceType.PAPER,
                InvoiceAppearance.Unknown, _ => Dto.InvoiceAppearanceType.UNKNOWN
            );
        }

        private static Dto.AdditionalDataType[] GetAdditionalInvoiceData(Invoice invoice)
        {

            if( invoice.AdditionalInvoiceData.Any())
            {
                return invoice.AdditionalInvoiceData.Select(x => new Dto.AdditionalDataType { dataDescription = x.DataDescription, dataName = x.DataName, dataValue = x.DataValue }).ToArray();
            }
            return null;
        }

        private static Dto.CustomerVatDataType GetCustomerVatDataType(TaxpayerIdentificationNumber taxpayerNumber)
        {
            return taxpayerNumber.Match(
                european => european.Country.Alpha2Code.Match(
                    Countries.Hungary.Alpha2Code, _ => new Dto.CustomerVatDataType
                    {
                        Item = new Dto.CustomerTaxNumberType
                        {
                            taxpayerId = taxpayerNumber.TaxpayerNumber
                        },
                        ItemElementName = Dto.ItemChoiceType.customerTaxNumber
                    },
                    _ => new Dto.CustomerVatDataType
                    {
                        Item = taxpayerNumber.TaxpayerNumber,
                        ItemElementName = Dto.ItemChoiceType.communityVatNumber
                    }
                ),
                nonEuropean => new Dto.CustomerVatDataType
                {
                    Item = taxpayerNumber.TaxpayerNumber,
                    ItemElementName = Dto.ItemChoiceType.thirdStateTaxId
                }
            );
        }

        private static Dto.SummaryNormalType MapTaxSummary(Invoice invoice, Models.Amount amount, Models.Amount amountHUF)
        {
            return new Dto.SummaryNormalType
            {
                invoiceNetAmount = amount.Net.Value,
                invoiceNetAmountHUF = amountHUF.Net.Value,
                invoiceVatAmount = amount.Tax.Value,
                invoiceVatAmountHUF = amountHUF.Tax.Value,
                summaryByVatRate = invoice.TaxSummary.Select(s => MapSummaryByVatRate(s)).ToArray()
            };
        }

        private static Dto.SummaryByVatRateType MapSummaryByVatRate(TaxSummaryItem taxSummary)
        {
            return new Dto.SummaryByVatRateType
            {
                vatRate = GetVatRate(taxSummary.TaxRate),
                vatRateNetData = new Dto.VatRateNetDataType
                {
                    vatRateNetAmount = taxSummary.Amount.Net.Value,
                    vatRateNetAmountHUF = taxSummary.AmountHUF.Net.Value
                },
                vatRateVatData = new Dto.VatRateVatDataType
                {
                    vatRateVatAmount = taxSummary.Amount.Tax.Value,
                    vatRateVatAmountHUF = taxSummary.AmountHUF.Tax.Value
                },
                vatRateGrossData = new Dto.VatRateGrossDataType
                {
                    vatRateGrossAmount = taxSummary.Amount.Gross.Value,
                    vatRateGrossAmountHUF = taxSummary.AmountHUF.Gross.Value
                }
            };
        }

        private static Dto.AddressType MapAddress(SimpleAddress address)
        {
            return new Dto.AddressType
            {
                Item = new Dto.SimpleAddressType
                {
                    additionalAddressDetail = address.AddtionalAddressDetail.Value,
                    city = address.City.Value,
                    countryCode = address.Country.Alpha2Code,
                    postalCode = address.PostalCode.Value,
                    region = address.Region.Map(r => r.Value).GetOrNull()
                }
            };
        }


        private static Dto.LineAmountsNormalType MapLineAmounts(InvoiceItem item)
        {
            return new Dto.LineAmountsNormalType
            {
                lineGrossAmountData = new Dto.LineGrossAmountDataType
                {
                    lineGrossAmountNormal = item.TotalAmounts.Amount.Gross.Value,
                    lineGrossAmountNormalHUF = item.TotalAmounts.AmountHUF.Gross.Value
                },
                lineNetAmountData = new Dto.LineNetAmountDataType
                {
                    lineNetAmount = item.TotalAmounts.Amount.Net.Value,
                    lineNetAmountHUF = item.TotalAmounts.AmountHUF.Net.Value
                },
                lineVatRate = GetVatRate(item.TotalAmounts.TaxRate),
                lineVatData = new Dto.LineVatDataType
                {
                    lineVatAmount = item.TotalAmounts.Amount.Tax.Value,
                    lineVatAmountHUF = item.TotalAmounts.AmountHUF.Tax.Value
                }
            };
        }

        private static Dto.DiscountDataType MapItemDiscount(InvoiceItem item)
        {
            if (item.ItemDiscount == null)
                return null;

            return new Dto.DiscountDataType
            {
                discountDescription = item.ItemDiscount.DiscountDescription,
                discountValue = item.ItemDiscount.DiscountNetTotal.GetOrZero(),
                discountValueSpecified = (item.ItemDiscount.DiscountNetTotal.GetOrZero() != 0),
                discountRate = item.ItemDiscount.DiscountPercentage.GetOrZero(),
                discountRateSpecified = (item.ItemDiscount.DiscountPercentage.GetOrZero() != 0),
            };
        }

        private static Dto.AdvanceDataType MapAdvanceData(InvoiceItem item)
        {
            if (item.AdvanceData == null)
                return null;

            return new Dto.AdvanceDataType
            {
                advanceIndicator = item.AdvanceData.AdvanceIndicator,
                advancePaymentData = item.AdvanceData.AdvanceOriginalInvoice.IsNull() ? null : new Dto.AdvancePaymentDataType
                {
                    advancePaymentDate = item.AdvanceData.AdvancePaymentDate,
                    advanceExchangeRate = item.AdvanceData.AdvanceExchangeRate.Value,
                    advanceOriginalInvoice = item.AdvanceData.AdvanceOriginalInvoice.Value
                }
            };
        }

        private static IEnumerable<Dto.LineType> MapItems(Invoice invoice, int? modificationIndexOffset = null)
        {
            ISequence<InvoiceItem> items = invoice.Items;
            var lines = items.Values.Select(i => new Dto.LineType
            {
                lineNumber = i.Index.ToString(),
                lineDescription = i.Value.Description.Value,
                quantity = i.Value.Quantity,
                unitOfMeasureOwn = i.Value.MeasurementUnit.ToString(),
                unitPrice = i.Value.UnitAmounts.Amount.Net.Value,
                unitPriceHUF = i.Value.UnitAmounts.AmountHUF.Net.Value,
                quantitySpecified = true,
                unitOfMeasureSpecified = true,
                unitPriceSpecified = true,
                unitPriceHUFSpecified = true,
                depositIndicator = i.Value.IsDeposit,
                Item = MapLineAmounts(i.Value),
                lineDiscountData = MapItemDiscount(i.Value),
                advanceData = MapAdvanceData(i.Value),
                aggregateInvoiceLineData = (invoice.InvoiceCategory.Get() != InvoiceCategory.AGGREGATE ? null : new Dto.AggregateInvoiceLineDataType
                {
                    lineExchangeRateSpecified = true,
                    lineExchangeRate = i.Value.ExchangeRate.Map(r => r.Value).GetOrElse(0m),
                    lineDeliveryDate = i.Value.ConsumptionDate
                }),
                lineModificationReference = modificationIndexOffset.HasValue ? GetLineModificationReference(i, modificationIndexOffset.Value) : null
            });

            return lines;
        }

        private static Dto.LineModificationReferenceType GetLineModificationReference(Indexed<InvoiceItem> item, int modificationIndexOffset)
        {
            return new Dto.LineModificationReferenceType
            {
                lineNumberReference = (item.Index + modificationIndexOffset).ToString(),
                lineOperation = Dto.LineOperationType.CREATE
            };
        }

        private static Dto.VatRateType GetVatRate(TaxRate taxRate)
        {
            return taxRate.TaxRateType.Match(
                TaxRateTypes.VatPercentage, _ => new Dto.VatRateType
                {
                    Item = taxRate.TaxRatePercentage.Get(),
                    ItemElementName = Dto.ItemChoiceType2.vatPercentage
                },
                TaxRateTypes.VatExemption, _ => new Dto.VatRateType
                {
                    Item = new Dto.DetailedReasonType { @case = taxRate.Case, reason = taxRate.Reason },
                    ItemElementName = Dto.ItemChoiceType2.vatExemption
                },
                TaxRateTypes.VatOutOfScope, _ => new Dto.VatRateType
                {
                    Item = new Dto.DetailedReasonType { @case = taxRate.Case, reason = taxRate.Reason },
                    ItemElementName = Dto.ItemChoiceType2.vatOutOfScope
                },
                TaxRateTypes.VatDomesticReverseCharge, _ => new Dto.VatRateType
                {
                    Item = true,
                    ItemElementName = Dto.ItemChoiceType2.vatDomesticReverseCharge
                },
                TaxRateTypes.NoVatCharge, _ => new Dto.VatRateType
                {
                    Item = true,
                    ItemElementName = Dto.ItemChoiceType2.noVatCharge
                }
            );
        }
    }
}
