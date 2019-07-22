using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SevensPizzaEntity
{
    [Table("CreditCards")]
    public class CreditCard
    {
        [Key]
        public int CardID { get; set; }

        [Required]
        [Display(Name = "Name on Card")]
        public string CardName { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Expiration")]
        public DataType DOE { get; set; }

        [Required]
        [Display(Name = "Security Code")]
        public int CecCode { get; set; }

        public int CustID { get; set; }

        public Customer Cust { get; set; }

    }
}
