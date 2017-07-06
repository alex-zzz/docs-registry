using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DocsRegistry.Common;
using DocsRegistry.Models;

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

        public JsonResult GetDocuments()
        {
            List<Document> docs = new List<Document>{
                new Document{Id = 1, Partner = "Client1", Prop1 = "Prop11", Prop2 = "Prop21", Status = false, Type = DocTypes.inDoc, Date = new DateTime(2011, 2, 14)},                new Document{Id = 1, Partner = "Client1", Prop1 = "Prop11", Prop2 = "Prop21", Status = false, Type = DocTypes.inDoc, Date = new DateTime(2011, 2, 14)},
                new Document{Id = 2, Partner = "Client2", Prop1 = "Prop12", Prop2 = "Prop22", Status = false, Type = DocTypes.inDoc, Date = new DateTime(2012, 2, 14)},                new Document{Id = 1, Partner = "Client1", Prop1 = "Prop11", Prop2 = "Prop21", Status = false, Type = DocTypes.inDoc, Date = new DateTime(2011, 2, 14)},
                new Document{Id = 3, Partner = "Client3", Prop1 = "Prop13", Prop2 = "Prop23", Status = true, Type = DocTypes.inDoc, Date = new DateTime(2013, 2, 14)},                new Document{Id = 1, Partner = "Client1", Prop1 = "Prop11", Prop2 = "Prop21", Status = false, Type = DocTypes.inDoc, Date = new DateTime(2011, 2, 14)},
                new Document{Id = 4, Partner = "Client4", Prop1 = "Prop14", Prop2 = "Prop24", Status = false, Type = DocTypes.outDoc, Date = new DateTime(2014, 2, 14)},                new Document{Id = 1, Partner = "Client1", Prop1 = "Prop11", Prop2 = "Prop21", Status = false, Type = DocTypes.inDoc, Date = new DateTime(2011, 2, 14)},
            };

            dynamic collectionWrapper = new
            {

                data = docs

            };

            var data = Helpers.ToJSON(collectionWrapper);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}