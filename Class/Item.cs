using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Class
{
    public class Item
    {
        public string Category { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public Dictionary<string, Type> ItemParaType { get; }
        public Item()
        {
            ItemParaType = new Dictionary<string, Type>
            {
                { nameof(Category),typeof(string)},
                { nameof(ItemName),typeof(string)},
                { nameof(Price),typeof(int)},
                { nameof(Amount),typeof(int)},
            };
        }
    }
}
