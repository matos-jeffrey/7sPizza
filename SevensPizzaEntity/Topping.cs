using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SevensPizzaEntity
{
    [Table("Toppings")]
    public class Topping
    {
        [Key]
        public int ToppingID { get; set; }

        public string ToppingType { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; } = false;
    }
}
