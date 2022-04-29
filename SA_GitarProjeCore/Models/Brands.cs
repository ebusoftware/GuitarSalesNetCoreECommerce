using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Models
{
    public class Brands
    {
        [Key]
        public int BrandId { get; set; }
        [Required]
        public string  BrandName { get; set; }
        public virtual List<Products> Products { get; set; } = new List<Products>();
    }
}
