using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator cal = new Calculator();
            var DiscountPath = ConfigurationManager.AppSettings["InputPath"].ToString();
            cal.getListDiscount(DiscountPath);
        }
    }
}
