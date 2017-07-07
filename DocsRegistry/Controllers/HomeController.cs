using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Soap;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using DocsRegistry.Common;
using DocsRegistry.Models;
using System.Xml.Serialization;

namespace DocsRegistry.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public JsonResult GetDocuments()
        //{
        //    List<Document> docs = new List<Document>{
        //        new Document{Id = 1, Partner = "Client1", Prop1 = "Prop11", Prop2 = "Prop21", Status = false, Type = DocTypes.inDoc, Date = new DateTime(2011, 2, 14)},                new Document{Id = 1, Partner = "Client1", Prop1 = "Prop11", Prop2 = "Prop21", Status = false, Type = DocTypes.inDoc, Date = new DateTime(2011, 2, 14)},
        //        new Document{Id = 2, Partner = "Client2", Prop1 = "Prop12", Prop2 = "Prop22", Status = false, Type = DocTypes.inDoc, Date = new DateTime(2012, 2, 14)},                new Document{Id = 1, Partner = "Client1", Prop1 = "Prop11", Prop2 = "Prop21", Status = false, Type = DocTypes.inDoc, Date = new DateTime(2011, 2, 14)},
        //        new Document{Id = 3, Partner = "Client3", Prop1 = "Prop13", Prop2 = "Prop23", Status = true, Type = DocTypes.inDoc, Date = new DateTime(2013, 2, 14)},                new Document{Id = 1, Partner = "Client1", Prop1 = "Prop11", Prop2 = "Prop21", Status = false, Type = DocTypes.inDoc, Date = new DateTime(2011, 2, 14)},
        //        new Document{Id = 4, Partner = "Client4", Prop1 = "Prop14", Prop2 = "Prop24", Status = false, Type = DocTypes.outDoc, Date = new DateTime(2014, 2, 14)},                new Document{Id = 1, Partner = "Client1", Prop1 = "Prop11", Prop2 = "Prop21", Status = false, Type = DocTypes.inDoc, Date = new DateTime(2011, 2, 14)},
        //    };

        //    dynamic collectionWrapper = new
        //    {

        //        data = docs

        //    };

        //    var data = Helpers.ToJSON(collectionWrapper);

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetDocumentsSoap()
        {

            InvokeService(1, 2);
            return new EmptyResult();
        }

        public static HttpWebRequest CreateSOAPWebRequest()
        {
            //Making Web Request    
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://lek.cloudua.net:8080/TestBase/ws/Documents.1cws");
            //SOAPAction    
            Req.Headers.Add(@"SOAPAction:http://www.TestBase.net/Documents#Documents:GetDocuments");

            String username = "obmen";
            String password = "obmen";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            Req.Headers.Add("Authorization", "Basic " + encoded);


            //Content_type    
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method    
            Req.Method = "POST";
            
            //return HttpWebRequest    
            return Req;
        }

        public static void InvokeService(int a, int b)
        {
            //Calling CreateSOAPWebRequest method    
            HttpWebRequest request = CreateSOAPWebRequest();

            XmlDocument SOAPReqBody = new XmlDocument();
            //SOAP Body Request    
            SOAPReqBody.LoadXml(@"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:doc=""http://www.TestBase.net/Documents"">
                                   <soap:Header/>
                                   <soap:Body>
                                      <doc:GetDocuments>
                                         <doc:Date1>2017-06-01</doc:Date1>
                                         <doc:Date2>2017-06-30</doc:Date2>
                                      </doc:GetDocuments>
                                   </soap:Body>
                                </soap:Envelope>");

            string ServiceResult;

            using (Stream stream = request.GetRequestStream())
            {
                SOAPReqBody.Save(stream);
            }
            //Geting response from request    
            using (WebResponse Serviceres = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                {
                    //reading stream    
                    ServiceResult = rd.ReadToEnd();
                    //writting stream result on console   

                }
            }

            //SoapFormatter formatter = new SoapFormatter();

            //var stream1 = GenerateStreamFromString(ServiceResult);
            //Document[] docs = (Document[])formatter.Deserialize(stream1);

            XDocument doc = XDocument.Parse(ServiceResult);
            XNamespace ns = "http://www.TestBase.net";
            var docs = doc.Descendants(ns + "Docs").ToList();

            List <Document> documents = new List<Document>();
            foreach (var item in docs)
            {
                StringReader reader = new StringReader(item.ToString());
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Document));
                documents.Add((Document)xmlSerializer.Deserialize(reader));
            }

            //var q1 = doc.Elements("Docs").ToList();

            //var items = from xe in doc.Element("Envelope").Element("Body").Element("GetDocumentsResponse").Element("return").Elements("Docs")
            //    select new Document()
            //    {
            //        Prop1 = xe.Attribute("Organization").Value,
            //        Prop2 = xe.Element("DocType").Value
            //    };

            //var hz = items.ToList();
        }

        public static Stream GenerateStreamFromString(string s) 
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}