using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Data
{
    [Table("Product_Cart")]
    public class Product_Cart
    {
        public int IdProduct { get; set; }
        public string? IdUser { get; set; }
        public int Quantity { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Column(TypeName = "date")]
        public DateTime? dateTime { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public Product product { get; set; }
    }
}
