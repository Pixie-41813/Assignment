using Assignment.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Parameter
    {
        public string[] TotalDiscountType;
        public string[] GoodsCategory;

        public Parameter(string[] DiscountType, string[] GoodsCategory)
        {
            this.TotalDiscountType = DiscountType;
            this.GoodsCategory = GoodsCategory;
        }

        public List<Discount> getListDiscount(string Path)
        {
            var ListDiscount = new List<Discount>();
            var DiscountPath = Path + "\\Discount.json";
            bool result = false;
            do
            {
                var cnt = 0;
                try
                {
                    if (File.Exists(DiscountPath))
                    {
                        var JsonReadText = File.ReadAllText(DiscountPath);
                        ListDiscount = JsonSerializer.Deserialize<List<Discount>>(JsonReadText);
                        if (isListDiscountCorrect(ListDiscount))
                        {
                            result = true;
                        }                                  
                    }
                    else
                    {
                        if (cnt > 0)
                        {
                            ListDiscount = new List<Discount>();
                            result = true;
                        }
                        else
                        {
                            Console.WriteLine("Do you have a dissount?");
                            Console.WriteLine("Please put discount file at " + Path + ".");
                            Console.WriteLine("Then Please press Enter to continue.");
                            Console.ReadLine();
                            cnt++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Discount discount = new Discount();
                    var errorparameter = string.Empty;
                    foreach (var a in discount.DiscountParaType)
                    {
                        if (ex.Message.Contains(a.Key))
                        {
                            errorparameter = a.Key;
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(errorparameter))
                    {
                        Console.WriteLine("Please fill value of " + errorparameter + " in type of " + discount.DiscountParaType[errorparameter].Name);
                    }
                    else
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("Please update you discount file.");
                    Console.WriteLine("Please Enter after revise the file.");
                    Console.ReadLine();
                }
            } while (!result);
            return ListDiscount;
        }

        private bool isDupDiscountType(List<Discount> ListDiscount)
        {
            var isDup = false;
            if (ListDiscount.Count <= TotalDiscountType.Length)
            {
                foreach (var a in TotalDiscountType)
                {
                    var Typecount = ListDiscount.Where(w => w.DiscountType == a).Count();
                    if (Typecount > 1)
                    {
                        isDup = true;
                        break;
                    }
                }
            }
            else
            {
                isDup = true;
            }
            return isDup;
        }

        private bool isCorrectedGoodsCategory(List<Discount> ListDiscount)
        {
            var isCorrect = true;
            var TotalTypeOntop = ListDiscount.Where(w => w.Campaign == "PercentageCat").Select(s => s).FirstOrDefault().Condition.Split(',');
            foreach (var a in TotalTypeOntop)
            {
                if (!GoodsCategory.Contains(a))
                {
                    isCorrect = false;
                    break;
                }
            }
            return isCorrect;
        }

        private bool isListDiscountCorrect(List<Discount> ListDiscount)
        {
            var iscorrected = false;
            if (isDupDiscountType(ListDiscount))
            {
                Console.WriteLine("Please remove duplicate discount type.");
                Console.WriteLine("Press Enter after revise.");
                Console.ReadLine();
            }
            else if (ListDiscount.Any(a => a.Campaign == "PercentageCat"))
            {
                if (!isCorrectedGoodsCategory(ListDiscount))
                {
                    Console.WriteLine("Please check goods type in parameter condition of Campaign PercentageCat");
                    Console.WriteLine("Press Enter after revise.");
                    Console.ReadLine();                   
                }
                else
                {
                    iscorrected = true;
                }
            }
            else
            {
                iscorrected = true;
            }
            return iscorrected;
        }

        public List<Item> getListItem(string Path)
        {
            var ListItem = new List<Item>();
            var ShoppingCartPath = Path + "\\ShoppingCart.json";
            bool existItemList = false;
            try
            {
                while (!File.Exists(ShoppingCartPath))
                {
                    Console.WriteLine("Please select item with your prefer.");
                    Console.Write("Do you update your shipping cart? [Y/N]: ");
                    existItemList = Console.ReadLine().ToUpper() == "Y" ? true : false;
                }

            }
            catch (Exception ex)
            {

            }
            return ListItem;
        }
    }
}
