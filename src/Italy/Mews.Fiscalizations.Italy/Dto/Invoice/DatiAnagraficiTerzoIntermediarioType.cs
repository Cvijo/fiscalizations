﻿using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class DatiAnagraficiTerzoIntermediarioType
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public SenderId IdFiscaleIVA { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string CodiceFiscale { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Identity Anagrafica { get; set; }
    }
}