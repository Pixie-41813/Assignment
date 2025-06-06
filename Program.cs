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
        public static string[] TotalDiscountType = new string[] { "Coupon", "OnTop", "Season" };
        public static string[] GoodsCategory = new string[] { "Clothing", "Accessories", "Gadget", "Cosmetic" };

        static void Main(string[] args)
        {
            Parameter cal = new Parameter(TotalDiscountType, GoodsCategory);
            var DiscountPath = ConfigurationManager.AppSettings["InputPath"].ToString();
            var ListDiscount = cal.getListDiscount(DiscountPath);
        }
    }
}
