using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Data
{
    [Table("Product_Bill")]
    public class Product_Bill
    {
        public int IdProduct { get; set; }
        public int IdBill { get; set; }
        [Range(0, 100)]
        public int Quantity { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public Product product { get; set; }
        public Bill bill { get; set; }

    }
}
