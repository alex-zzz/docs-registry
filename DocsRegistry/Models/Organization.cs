using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace DocsRegistry.Models
{
    [Serializable()]
    [XmlType("Organization")]
    [XmlRoot(ElementName = "Organization", Namespace = "http://www.TestBase.net")] 
    public class Organization
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("EDRPOU")]
        public string EDRPOU { get; set; }
    }
}