using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Part_B.Tests
{
    [TestClass]
    public class AssemblyConfig
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            //  Define data directory
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database"));
        }
    }
}
