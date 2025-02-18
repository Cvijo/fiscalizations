﻿using System;
using System.Xml;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("SignatureMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class SignatureMethod
    {
        [XmlElement(DataType = "integer")]
        public string HMACOutputLength { get; set; }

        [XmlText, XmlAnyElement]
        public XmlNode[] Any { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string Algorithm { get; set; }
    }
}