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
        public List<Discount> getListDiscount(string Path)
        {
            var ListDiscount = new List<Discount>();
            var DiscountPath = Path + "\\Discount.json";

            bool existDiscount = false;

            try
            {
                if (!File.Exists(DiscountPath))
                {
                    Console.WriteLine("Please Save Discount.json File to " + Path + ", or you will not get discount.");
                    Console.Write("Do you put your discount? [Y/N]: ");
                    existDiscount = Console.ReadLine().ToUpper() == "Y" ? true : false;
                }
                else
                {
                    existDiscount = true;
                }

                if (existDiscount)
                {
                    var Readjson = File.ReadAllText(DiscountPath);
                    ListDiscount = JsonSerializer.Deserialize<List<Discount>>(Readjson);                    
                }               
            }
            catch (Exception ex)
            {
                var AllDiscountParameter = new string[] { "Campaign", "DiscountType", "DiscountAmount", "Condition" };
                var errorparameter = string.Empty;
                foreach (var a in AllDiscountParameter)
                {
                    if (ex.Message.Contains(a))
                    {
                        errorparameter = a;
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(errorparameter))
                {
                    Discount ParaDict = new Discount();
                    var inputtype = ParaDict.DiscountParaType[errorparameter];
                    Console.WriteLine("Please set parameter " + errorparameter + " in type of: "+inputtype.Name);
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }
            return ListDiscount;
        }

        public List<Item> getListItem(string Path)
        {
            var ListItem = new List<Item>();
            return ListItem;
        }
    }
}
