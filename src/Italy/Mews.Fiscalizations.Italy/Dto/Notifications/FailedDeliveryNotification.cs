﻿using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Italy.Dto.Notifications
{
    [XmlType(Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0")]
    [XmlRoot("NotificaMancataConsegna", Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0", IsNullable = false)]
    public class FailedDeliveryNotification : SdiNotification
    {
        [XmlElement("DataOraRicezione", Form = XmlSchemaForm.Unqualified)]
        public DateTime Received { get; set; }

        [XmlElement("RiferimentoArchivio", Form = XmlSchemaForm.Unqualified)]
        public InvoiceArchive InvoiceArchive { get; set; }

        [XmlElement("Descrizione", Form = XmlSchemaForm.Unqualified)]
        public string Description { get; set; }

        [XmlElement("PecMessageId", Form = XmlSchemaForm.Unqualified)]
        public string CemMessageId { get; set; }
    }
}