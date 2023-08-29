using FuncSharp;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class AdditionalInvoiceData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataName">Unique ID of data field Example:A00001_RENDELES_SZAM, X00002_SHIPMENT_ID, X00999_SHIPMENT_VOLUME_M3</param>
        /// <param name="dataDescription"></param>
        /// <param name="dataValue"></param>
        public AdditionalInvoiceData(string dataName, string dataDescription, string dataValue)
        {
            DataName = dataName;
            DataDescription = dataDescription;
            DataValue = dataValue;
            //TaxRatePercentage = taxRatePercentage.ToOption();
            
        }

        public string DataName { get; }
        public string DataDescription { get; }
        public string DataValue { get; }

    }
}
