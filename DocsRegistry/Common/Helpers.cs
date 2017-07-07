using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace DocsRegistry.Common
{
    public static class Helpers
    {
        public static string ToJSON(this object obj)
        {

            return JsonConvert.SerializeObject(obj, new Newtonsoft.Json.Converters.StringEnumConverter());
            //var serializer = new JavaScriptSerializer();

            //return serializer.Serialize(obj);
        }

        public static string ToJSON(this object obj, int recursionDepth)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }

        public static T SOAPToObject<T>(string SOAP)
        {
            if (string.IsNullOrEmpty(SOAP))
            {
                throw new ArgumentException("SOAP can not be null/empty");
            }
            try
            {
                using (MemoryStream Stream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(SOAP)))
                {
                    SoapFormatter Formatter = new SoapFormatter();
                    return (T)Formatter.Deserialize(Stream);
                }
            }
            catch (Exception e)
            { throw e; }
        }
    }


}