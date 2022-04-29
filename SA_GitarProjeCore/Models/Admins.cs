using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Models
{
    public class Admins
    {
        [Key]
        public int AdminId { get; set; }
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez.")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre Boş Geçilemez!")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Mail Boş Geçilemez!")]
        public string E_Mail { get; set; }

    }
}
