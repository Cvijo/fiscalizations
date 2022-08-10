using System;

namespace Mews.Eet.Tests
{
    public class Fixtures
    {
        public static readonly string TaxId1 = "CZ1212121218";
        public static readonly string TaxId2 = "CZ00000019";
        public static readonly string TaxId3 = "CZ683555118";
        public static readonly string CertificateData1 = "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";
        public static readonly string CertificateData2 = "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";
        public static readonly string CertificateData3 = "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";
        public static readonly string CertificatePassword = "eet";

        public static TaxPayerFixture First = new TaxPayerFixture
        {
            TaxId = TaxId1,
            PremisesId = 1,
            CertificatePassword = CertificatePassword,
            CertificateData = Convert.FromBase64String(CertificateData1)
        };

        public static TaxPayerFixture Second = new TaxPayerFixture
        {
            TaxId = TaxId2,
            PremisesId = 1,
            CertificatePassword = CertificatePassword,
            CertificateData = Convert.FromBase64String(CertificateData2)
        };

        public static TaxPayerFixture Third = new TaxPayerFixture
        {
            TaxId = TaxId3,
            PremisesId = 1,
            CertificatePassword = CertificatePassword,
            CertificateData = Convert.FromBase64String(CertificateData3)
        };
    }

    public class TaxPayerFixture
    {
        public string TaxId { get; set; }
        public int PremisesId { get; set; }
        public string CertificatePassword { get; set; }
        public byte[] CertificateData { get; set; }
    }
}
