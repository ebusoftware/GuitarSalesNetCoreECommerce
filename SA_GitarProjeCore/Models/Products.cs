using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        [Required]
        [ForeignKey("Brands")]
        public int BrandId{ get; set; }
        [ForeignKey("Bodys")]
        public int BodyId{ get; set; }
        [Required]
        [ForeignKey("Wires")]
        public int WireId{ get; set; }
        
        [ForeignKey("Colors")]
        public int ColorId { get; set; }
        public string BarcodeNumber { get; set; }
        [Required]
        public string ProductName{ get; set; }
        [Required]
        public float Price{ get; set; }
        [Required]
        public int StockQuantity{ get; set; }
        public string Comment{ get; set; }
        [NotMapped]
        [DisplayName("Upload File")]

        [Required(ErrorMessage = "Lütfen fotoğraf giriniz!")]
        public IFormFile ImageFile { get; set; }
        [DisplayName("Upload File")]
        public string ImageName{ get; set; }

        public virtual Categories Categories { get; private set; }
        public virtual Brands Brands { get; private set; }
        public virtual Bodies Bodys { get; private set; }
        public virtual Wires Wires { get; private set; }
        public virtual Colors Colors { get; private set; }
    
        public List<Sales> Sales{ get; set; }


    }
}
