using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Class
{
    public class Discount
    {
        public string Campaign { get; set; }
        public string DiscountType { get; set; }
        public int DiscountAmount { get; set; }
        public string Condition { get; set; }

        public Dictionary<string, Type> DiscountParaType { get; }
        public Discount()
        {
            DiscountParaType = new Dictionary<string, Type>
            {
                { nameof(Campaign),typeof(string)},
                { nameof(DiscountType),typeof(string)},
                { nameof(DiscountAmount),typeof(int)},
                { nameof(Condition),typeof(string)},
            };
        }
    }
}
