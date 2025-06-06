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
            var cnt = 0;

            do
            {
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
                            Console.WriteLine("We cannot find your Discouint file.");
                            var isCorrecttedKey = false;
                            do
                            {
                                Console.Write("Do you still want to add discount? [Y/N]");
                                var ans = Console.ReadLine().ToUpper();
                                if (ans == "Y" || ans == "N")
                                {
                                    isCorrecttedKey = true;
                                    if (ans == "Y")
                                    {
                                        cnt++;
                                        Console.WriteLine("Please press Enter after place the file.");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        ListDiscount = new List<Discount>();
                                        result = true;
                                    }
                                }
                            } while (!isCorrecttedKey);
                        }
                        else
                        {
                            Console.WriteLine("Do you have a dicount?");
                            Console.WriteLine("Please put discount file at " + Path + ".");
                            var isCorrecttedKey = false;
                            do
                            {
                                Console.Write("Press \"Y\" if Yes, Press \"N\" if No: ");
                                var ans = Console.ReadLine().ToUpper();
                                if (ans == "Y" || ans == "N")
                                {
                                    isCorrecttedKey = true;
                                    if (ans == "Y")
                                    {
                                        cnt++;
                                        Console.WriteLine("Please press Enter after place the file.");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        ListDiscount = new List<Discount>();
                                        result = true;
                                    }
                                }
                            } while (!isCorrecttedKey);
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
                        Console.WriteLine("Please revise your discount file at parameter " + errorparameter + " to a type of " + discount.DiscountParaType[errorparameter].Name);
                    }
                    else
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Please revise your discount file.");
                    }
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
                    Console.WriteLine("Please check goods type in parameter condition of campaign PercentageCat");
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

        public Dictionary<string, double> getDictGoodsPrice(string Path)
        {
            var ListItem = new List<Item>();
            var DictGoodsPrice = new Dictionary<string, double>();
            var ShoppingCartPath = Path + "\\ShoppingCart.json";
            var result = false;
            do
            {
                try
                {
                    if (File.Exists(ShoppingCartPath))
                    {
                        var JsonReadText = File.ReadAllText(ShoppingCartPath);
                        ListItem = JsonSerializer.Deserialize<List<Item>>(JsonReadText);
                        foreach (var a in GoodsCategory)
                        {
                            var Item = ListItem.Where(w => w.Category == a).Select(s => s).ToList();
                            if (Item.Count > 0)
                            {
                                var TotalAmount = Item.Select(s => s.Amount * s.Price).Sum();
                                DictGoodsPrice.Add(a, TotalAmount);
                            }                            
                        }
                        result = true;
                    }
                    else
                    {
                        var isCorrectedKey = false;
                        Console.WriteLine("Do you have a shipping cart file?");
                        do
                        {
                            Console.Write("Press \"Y\" if Yes, Press \"N\" if No: ");
                            var ans = Console.ReadLine().ToUpper();
                            if (ans == "Y" || ans == "N")
                            {
                                isCorrectedKey = true;
                                if (ans == "Y")
                                {
                                    Console.WriteLine("Please put your shipping cart file at " + Path + ".");
                                    Console.WriteLine("Press Enter after put file in the location.");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("Do you confirm to exit program");
                                    var isexitprogram = false;
                                    do
                                    {
                                        Console.Write("Press \"Y\" if Yes, Press \"N\" if No: ");
                                        ans = Console.ReadLine().ToUpper();
                                        if (ans == "Y" || ans == "N")
                                        {
                                            isexitprogram = true;
                                            if (ans == "Y")
                                            {
                                                DictGoodsPrice = new Dictionary<string, double>();
                                                result = true;
                                            }                                           
                                        }
                                    } while (!isexitprogram);
                                }
                            }
                        } while (!isCorrectedKey);
                    }
                }
                catch (Exception ex)
                {
                    Item item = new Item();
                    var errorparameter = string.Empty;
                    foreach (var a in item.ItemParaType)
                    {
                        if (ex.Message.Contains(a.Key))
                        {
                            errorparameter = a.Key;
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(errorparameter))
                    {
                        Console.WriteLine("Please revise your shopping cart file at " + errorparameter + " to a type of " + item.ItemParaType[errorparameter].Name);
                    }
                    else
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Please revise your shopping cart file.");
                    }
                    Console.WriteLine("Please Enter after revise the file.");
                    Console.ReadLine();
                }
            } while (!result);
            return DictGoodsPrice;
        }
    }
}
