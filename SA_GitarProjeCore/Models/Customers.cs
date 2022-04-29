using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }
        [ForeignKey("Citys")]
        public int CityId { get; set; }
        
        public string Addresses { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber{ get; set; }
        public string TaxNumber{ get; set; }
        public string Email{ get; set; }
        //
        public virtual Cities Citys { get; private set; }

        public virtual List<Sales> Sales { get; set; } = new List<Sales>();

    }
}
