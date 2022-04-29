using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Models
{
    public class Sales
    {
        [Key]
        public int SaleId { get; set; }
        [Required]
        [ForeignKey("Products")]
        public int ProductId { get; set; }
        [Required]
        [ForeignKey("Customers")]
        public int CustomerId { get; set; }
        [Column(TypeName ="money")]
        public float UnitPrice { get; set; }
        public int Number{ get; set; }
        public DateTime Date{ get; set; }

        public virtual Products Products { get; private set; }
        public virtual Customers Customers { get; private set; }
    }
}
