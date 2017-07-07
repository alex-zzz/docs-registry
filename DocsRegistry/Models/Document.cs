using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DocsRegistry.Models
{
    [Serializable]
    public class Document
    {
        //public int Id { get; set; }
        //public string Partner { get; set; }
        //public bool Status { get; set; }
        //public string Prop1 { get; set; }
        //public string Prop2 { get; set; }

        //public DocTypes Type { get; set; }
        //public DateTime? Date { get; set; }

        public string DocType { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public string Comments { get; set; }
        public Organization Organization { get; set; }
        public Customer Customer { get; set; }
    }

    public enum DocTypes
    {
        inDoc,
        outDoc
    }

}