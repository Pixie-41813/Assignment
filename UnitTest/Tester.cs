using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment.UnitTest
{
    [TestClass]
    public class Tester
    {
        [TestMethod]
        public void TestDiscountRead()
        {
            Calculator cal = new Calculator();
            var DiscountPath = ConfigurationManager.AppSettings["DiscountPath"].ToString();
            cal.getListDiscount(DiscountPath);
        }
    }
}
