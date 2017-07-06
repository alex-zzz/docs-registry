using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}