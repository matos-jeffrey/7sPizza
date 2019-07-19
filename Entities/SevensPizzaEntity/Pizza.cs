using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SevensPizzaEntity
{
    [Table("Pizzas")]
    public class Pizza
    {
        [Key]
        public int PizzaID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        [Display(Name = "Crust Style")]
        public string CrustStyle { get; set; }

        [Required]
        public string Sauce { get; set; }

        [Required]
        [Display(Name = "How Much Sauce?")]
        public string SauceAmount { get; set; }

        [Required]
        [Display(Name = "How Much Cheese")]
        public string CheeseAmount { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int OrderID { get; set; }

        public Order order { get; set; }

        public string Toppings { get; set; }

        [NotMapped]
        public List<Topping> ToppingsList { get; set; }

    }
}
