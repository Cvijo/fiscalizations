﻿namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
    public class RespuestaConsultaLRDetOperIntracomunitariasType : RespuestaConsultaLRFacturasType
    {
        [System.Xml.Serialization.XmlElementAttribute("RegistroRespuestaConsultaLRDetOperIntracomunitarias", Order = 0)]
        public RegistroRespuestaConsultaDetOperIntracomunitariasType[] RegistroRespuestaConsultaLRDetOperIntracomunitarias { get; set; }
    }
}