﻿namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class IDFacturaARType
    {
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string NumSerieFacturaEmisor { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string FechaExpedicionFacturaEmisor { get; set; }
    }
}