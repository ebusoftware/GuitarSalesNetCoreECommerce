using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Models
{
    public class Cities
    {
        [Key]
        public int CityId { get; set; }
        [Required]
        public string CityName { get; set; }
        public virtual List<Customers> Customers { get; set; } = new List<Customers>();
        public virtual List<Users> Users { get; set; } = new List<Users>();
        
        
    }
}
