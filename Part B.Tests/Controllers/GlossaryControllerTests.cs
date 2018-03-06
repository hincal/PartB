using Kendo.Mvc.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Part_B.Models;
using Part_B.Tests;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Part_B.Controllers.Tests
{
    [TestClass()]
    public class GlossaryControllerTests : TestBase
    {
        public GlossaryController controller = new GlossaryController();

        [TestMethod()]
        public void IndexTest()
        {
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Glossary", result.ViewData["Name"]);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var item = new Glossary { Term = "Term1", Definition = "Def1" };

            var result = (JsonResult)controller.Create(new DataSourceRequest(), item);

            using (var db = new GlossaryEntities())
            {
                var glossaryRecords = db.Glossary.ToList();
                
                Assert.IsNotNull(glossaryRecords);
                Assert.AreEqual(glossaryRecords.Count, 1);
                Assert.AreEqual(glossaryRecords.First().Term, "Term1");
                Assert.AreEqual(glossaryRecords.First().Definition, "Def1");
            }
        }

        [TestMethod()]
        public void ReadTest()
        {
            using (var db = new GlossaryEntities())
            {
                db.Glossary.Add(new Glossary { Term = "Term1", Definition = "Def1" });
                db.Glossary.Add(new Glossary { Term = "Term2", Definition = "Def2" });
                db.SaveChanges();
            }

            var result = (JsonResult)controller.Read(new DataSourceRequest());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(DataSourceResult));
            Assert.IsNotNull(result.Data);
            Assert.IsInstanceOfType(((DataSourceResult)result.Data).Data, typeof(IEnumerable));
            Assert.IsNotNull(((DataSourceResult)result.Data).Data);

            Assert.AreEqual(((List<Glossary>)((DataSourceResult)result.Data).Data).Count, 2);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            using (var db = new GlossaryEntities())
            {
                db.Glossary.Add(new Glossary { Term = "Term1", Definition = "Def1" });
                db.SaveChanges();

                var item = db.Glossary.First();
                item.Term = "Term2";

                var result = (JsonResult)controller.Update(new DataSourceRequest(), item);

                var item2 = db.Glossary.FirstOrDefault(q => q.Id == item.Id);

                Assert.IsNotNull(item2);
                Assert.AreEqual(item2.Term, "Term2");
            }
        }

        [TestMethod()]
        public void DeleteTest()
        {
            using (var db = new GlossaryEntities())
            {
                db.Glossary.Add(new Glossary { Term = "Term1", Definition = "Def1" });
                db.Glossary.Add(new Glossary { Term = "Term2", Definition = "Def2" });
                db.SaveChanges();

                var item = db.Glossary.First();

                var result = (JsonResult)controller.Delete(new DataSourceRequest(), item);

                var item2 = db.Glossary.FirstOrDefault(q => q.Id == item.Id);

                Assert.IsNull(item2);
            }
        }
    }
}