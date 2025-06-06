using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Class
{
    public class Calculator
    {
        public static string[] TotalDiscountType = new string[] { "Coupon", "OnTop", "Season" };
        public static string[] GoodsCategory = new string[] { "Clothing", "Accessories", "Gadget", "Cosmetic" };

        public void CalculateTotalPrice()
        {
            Parameter parameter = new Parameter(TotalDiscountType, GoodsCategory);
            var ParameterPath = ConfigurationManager.AppSettings["InputPath"].ToString();
            try
            {
                var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.FullName;
                var fullPath = Path.Combine(projectRoot ?? "", ParameterPath);
                var DictGoodsPrice = parameter.getDictGoodsPrice(fullPath);
                var ListDiscount = new List<Discount>();
                if (DictGoodsPrice.Count() != 0)
                {
                    ListDiscount = parameter.getListDiscount(fullPath);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
