﻿using System.Xml.Serialization;

namespace Mews.Fiscalizations.Spain.Dto.Responses.SoapFault
{
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public partial class SoapFaultResultBody
    {
        public SoapFaultResultBodyFault Fault { get; set; }
    }
}
