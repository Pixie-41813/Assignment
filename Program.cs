using Assignment.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Program
    {    
        static void Main(string[] args)
        {
            var DictGoodsPrice = new Dictionary<string, double>();
            var DictDiscountPrice = new Dictionary<string, double>();
            Calculator cal = new Calculator();
            cal.getDetailforCalculate(DictGoodsPrice, DictDiscountPrice);
        }      
    }
}
