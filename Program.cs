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
            Calculator cal = new Calculator();
            cal.CalculateTotalPrice();
        }      
    }
}
