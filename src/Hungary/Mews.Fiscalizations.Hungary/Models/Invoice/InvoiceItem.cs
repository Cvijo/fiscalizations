using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System;
using System.Collections.Generic;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class InvoiceItem
    {
        public InvoiceItem(
            DateTime consumptionDate,
            ItemAmounts totalAmounts,
            ItemAmounts unitAmounts,
            MeasurementUnit measurementUnit,
            Description description,
            int quantity,
            ExchangeRate exchangeRate = null,
            bool isDeposit = false,
            ItemDiscount itemDiscount = null,
            AdvanceData advanceData = null,
            List<AdditionalLineData> additionalLineData = null)
        {
            ConsumptionDate = consumptionDate;
            TotalAmounts = Check.IsNotNull(totalAmounts, nameof(totalAmounts));
            UnitAmounts = Check.IsNotNull(unitAmounts, nameof(unitAmounts));
            MeasurementUnit = measurementUnit;
            Description = Check.IsNotNull(description, nameof(description));
            Quantity = quantity;
            ExchangeRate = exchangeRate.ToOption();
            IsDeposit = isDeposit;
            ItemDiscount = itemDiscount;
            AdvanceData = advanceData;
            AdditionalLineData = additionalLineData;
        }

        public DateTime ConsumptionDate { get; }

        public ItemAmounts TotalAmounts { get; }

        public ItemAmounts UnitAmounts { get; }

        public MeasurementUnit MeasurementUnit { get; }

        public Description Description { get; }

        public int Quantity { get; }

        public IOption<ExchangeRate> ExchangeRate { get; }

        public bool IsDeposit { get; }
        public ItemDiscount ItemDiscount { get; }
        public AdvanceData AdvanceData { get; }
        public List<AdditionalLineData> AdditionalLineData { get; }
    }
}
