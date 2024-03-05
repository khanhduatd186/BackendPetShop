using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Data
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Tittle { get; set; }
        [MaxLength(int.MaxValue)]
        public string? Description { get; set; }
        [MaxLength(int.MaxValue)]
        public string Image { get; set; }
        [Range(0, 100)]
        public int Quantity { get; set; }
        [Range(0,double.MaxValue)]
        public double Price { get; set; }
        public byte Isdelete { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public ICollection<Product_Bill> product_Bills { get; set; }
        public ICollection<Product_Cart> product_Carts { get; set; }
        public Product()
        {
            product_Bills = new List<Product_Bill>();
            product_Carts = new List<Product_Cart>();
        }


    }
}
