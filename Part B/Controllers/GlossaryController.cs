using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Part_B.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Part_B.Controllers
{
    public class GlossaryController : Controller
    {
        private GlossaryEntities db = new GlossaryEntities();

        // GET: Glossary
        public ActionResult Index()
        {
            ViewData["Name"] = "Glossary";
            return View();
        }

        // CREATE: Glossary
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, Glossary item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Glossary.Add(item);
                    db.SaveChanges();
                    return Json(new[] { item }.ToDataSourceResult(request, ModelState));
                }
                else
                {
                    return Json(db.Glossary.ToList());
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        // READ: Glossary
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var glossary = db.Glossary.ToList();

                return Json(glossary.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        // UPDATE: Glossary
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, Glossary item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new[] { item }.ToDataSourceResult(request, ModelState));
                }
                else
                {
                    return Json(db.Glossary.ToList());
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        // DELETE: Glossary
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, Glossary item)
        {
            try
            {
                var glossary = db.Glossary.Find(item.Id);
                if (glossary == null)
                {
                    return Json("Item Not Found");
                }

                db.Glossary.Remove(glossary);
                db.SaveChanges();
                return Json(db.Glossary.ToList());
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public void Dispose()
        {
            db.Dispose();
            base.Dispose();
        }
    }
}
