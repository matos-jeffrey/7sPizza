using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevensPizzaAPI.Model
{
    //this class is for getting data from ajax call in shopping cart page
    public class QuantityUpdate
    {
        public int pid { get; set; }
        public int oid { get; set; }
        public int quantity { get; set; }
    }
}
