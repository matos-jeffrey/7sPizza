using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevensPizzaAPI.Model
{
    //this class is for return value to the shoppingCart
    public class PizzaAndOrder
    {
        public decimal PizzaPrice { get; set; }
        public int OrderTotalQuantity { get; set; }
        public decimal OrderTotalPrice { get; set; }
    }
}
