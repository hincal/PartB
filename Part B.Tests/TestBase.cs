using Microsoft.VisualStudio.TestTools.UnitTesting;
using Part_B.Models;
using System.Linq;

namespace Part_B.Tests
{
    public abstract class TestBase
    {
        [TestInitialize]
        public void TestInitialize()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var db = new GlossaryEntities())
            {
                foreach (var item in db.Glossary.ToList())
                    db.Glossary.Remove(item);
                db.SaveChanges();
            }
        }
    }
}
