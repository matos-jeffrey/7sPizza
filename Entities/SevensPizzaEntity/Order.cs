using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SevensPizzaEntity
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public int TotalPizza { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderTime { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; } = "Cash";

        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }

        [Required]
        public bool Delivery { get; set; } = false;

        public bool Checkout { get; set; } = false;

        public int CustID { get; set; }

        public int? CardID { get; set; }

        public Customer Cust { get; set; }

        public CreditCard Card { get; set; }

        public List<Pizza> PizzaList { get; set; }
    }
}
