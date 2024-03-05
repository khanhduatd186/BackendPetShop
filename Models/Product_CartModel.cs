using ApiPetShop.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Models
{
    public class Product_CartModel
    {
        public int IdProduct { get; set; }
  
        public string? IdUser { get; set; }
        public int Quantity { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Column(TypeName = "date")]
        public DateTime? dateTime { get; set; }
   
    }

}
