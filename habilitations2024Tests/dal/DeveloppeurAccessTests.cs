using Microsoft.VisualStudio.TestTools.UnitTesting;
using habilitations2024.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habilitations2024.dal.Tests
{
    [TestClass()]
    public class DeveloppeurAccessTests
    {
        habilitations2024.dal.DeveloppeurAccess dev = new DeveloppeurAccess();

        [TestMethod()]
        public void GetLesDeveloppeursTest1()
        {
            Assert.AreEqual(4, dev.GetLesDeveloppeurs("admin").Count);
        }

        [TestMethod()]
        public void GetLesDeveloppeursTest2()
        {
            Assert.AreEqual(14, dev.GetLesDeveloppeurs("").Count);
        }
    }
}