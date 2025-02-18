﻿using System;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("Manifest", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class Manifest
    {
        [XmlElement("Reference")]
        public Reference[] Reference { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id { get; set; }
    }
}