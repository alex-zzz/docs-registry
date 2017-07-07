using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace DocsRegistry.Models
{
    [Serializable()]
    [XmlType("Customer")]
    [XmlRoot(ElementName = "Customer", Namespace = "http://www.TestBase.net")]
    public class Customer
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("EDRPOU")]
        public string EDRPOU { get; set; }
    }
}