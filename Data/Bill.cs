using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Data
{
    [Table("Bill")]
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
     
        public DateTimeOffset dateTime { get; set; }
        public int Status { get; set; }
        public byte IsDelete { get; set; }
        public string? IdUser { get; set; }
        [ForeignKey("IdUser")]
        public ApplicationUser applicationUser { get; set; }
        public  ICollection<Product_Bill> product_Bills { get; set; }
        public  ICollection<Service_Bill>Service_Bills { get; set; }
        public Bill()
        {
            product_Bills = new List<Product_Bill>();
            Service_Bills = new List<Service_Bill>();
        }



    }
}
