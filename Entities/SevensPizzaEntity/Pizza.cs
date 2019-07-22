using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevensPizzaEntity
{
    [Table("Pizzas")]
    public class Pizza
    {
        [Key]
        public int PizzaID { get; set; }

        [Required]
        public string PizzaName { get; set; } = "Custom";

        [Required]
        public int Quantity { get; set; } = 1;

        [Required]
        public string Size { get; set; } = "Medium";

        [Required]
        [Display(Name = "Crust Style")]
        public string CrustStyle { get; set; } = "Original";

        [Required]
        public string Sauce { get; set; } = "Tomato";

        [Required]
        [Display(Name = "How Much Sauce?")]
        public string SauceAmount { get; set; } = "Normal";

        [Required]
        [Display(Name = "How Much Cheese")]
        public string CheeseAmount { get; set; } = "Normal";

        [Required]
        public decimal Price { get; set; }

        public int OrderID { get; set; }

        public Order order { get; set; }

        public string Toppings { get; set; }

        [NotMapped]
        public List<Topping> Meats { get; set; }

        [NotMapped]
        public List<Topping> Veggies { get; set; }

        public string ToppingList()
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in Meats)
            {
                if (item.IsSelected == true)
                    str.Append(item.Name + ",");
            }
            foreach (var item in Veggies)
            {
                if (item.IsSelected == true)
                    str.Append(item.Name + ",");
            }
            //save value to Toppings 
            Toppings = str.ToString().Substring(0, (str.Length > 0) ? str.Length - 1 : 0);
            return Toppings;
        }
    }
}
