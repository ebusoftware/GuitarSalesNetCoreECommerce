
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }

        public string Address { get; set; }
        [ForeignKey("Citys")]
        [Required]
        public int CityId { get; set; }
        [Required]
        public string E_mail { get; set; }

        public virtual Cities Citys { get; private set; }
    }
}
