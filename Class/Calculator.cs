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
        public static string[] TotalDiscountType = new string[] { "Coupon", "OnTop", "Season" }; //Ordered
        public static string[] GoodsCategory = new string[] { "Clothing", "Accessories", "Gadget", "Cosmetic" };

        public void getDetailforCalculate(Dictionary<string, double> DictGoodsPrice, Dictionary<string, double> DictDiscountPrice)
        {
            Parameter parameter = new Parameter(TotalDiscountType, GoodsCategory);
            var ParameterPath = ConfigurationManager.AppSettings["InputPath"].ToString();
            try
            {
                var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.FullName;
                var fullPath = Path.Combine(projectRoot ?? "", ParameterPath);
                parameter.getDictGoodsPrice(fullPath, DictGoodsPrice);
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
        public void DiscountCalculator(Dictionary<string, double> DictGoodsPrice, List<Discount> ListDiscount, Dictionary<string, double> DictDiscountPrice)
        {
            foreach (var a in DictGoodsPrice)
            {
                DictDiscountPrice.Add(a.Key, 0);
            }

            var CurrentDiscount = DictDiscountPrice;

            foreach (var discounttype in TotalDiscountType)
            {                
                if (ListDiscount.Any(a => a.DiscountType == discounttype))
                {
                    var discountdetail = ListDiscount.Where(w => w.DiscountType == discounttype).Select(s => s).FirstOrDefault();
                    if (discounttype == "Coupon")
                    {
                        if (discountdetail.Campaign == "Fix Amount")
                        {
                            var AvgDiscountPerCat = Math.Round(Convert.ToDouble(discountdetail.DiscountAmount / (DictDiscountPrice.Keys.Count() - 1)), 2);
                            foreach (var dis in DictDiscountPrice)
                            {
                                if (dis.Key == "Total")
                                {
                                    DictDiscountPrice[dis.Key] += discountdetail.DiscountAmount;
                                    CurrentDiscount[dis.Key] = discountdetail.DiscountAmount;
                                }
                                else
                                {
                                    DictDiscountPrice[dis.Key] += AvgDiscountPerCat;
                                    CurrentDiscount[dis.Key] = AvgDiscountPerCat;
                                }
                            }
                        }
                        else
                        {
                            foreach (var dis in DictDiscountPrice)
                            {
                                DictDiscountPrice[dis.Key] += Math.Round(Convert.ToDouble((DictGoodsPrice[dis.Key] * discountdetail.DiscountAmount) / 100.0));
                                CurrentDiscount[dis.Key] = Math.Round(Convert.ToDouble((DictGoodsPrice[dis.Key] * discountdetail.DiscountAmount) / 100.0));
                            }
                        }
                    }
                    else if (discounttype == "OnTop")
                    {
                        if (discountdetail.Campaign == "PercentageCat")
                        {
                            var GoodsDiscount = discountdetail.Condition.Split(',');
                            foreach (var a in GoodsDiscount)
                            {
                                DictDiscountPrice[a] += Math.Round(Convert.ToDouble((DictGoodsPrice[a] * discountdetail.DiscountAmount) / 100.0));
                                DictDiscountPrice["Total"] += Math.Round(Convert.ToDouble((DictGoodsPrice[a] * discountdetail.DiscountAmount) / 100.0));

                                CurrentDiscount[a] = Math.Round(Convert.ToDouble((DictGoodsPrice[a] * discountdetail.DiscountAmount) / 100.0));
                                CurrentDiscount["Total"] = Math.Round(Convert.ToDouble((DictGoodsPrice[a] * discountdetail.DiscountAmount) / 100.0));
                            }
                        }
                        else
                        {
                            var CapDiscount = DictGoodsPrice["Total"] * 0.2;
                            var realDiscount = CapDiscount;
                            if (discountdetail.DiscountAmount >= CapDiscount)
                            {

                            }
                        }
                    }
                }
            }
        }

        public void UpdateDictGoodsPrice()
        {

        }
    }
}
