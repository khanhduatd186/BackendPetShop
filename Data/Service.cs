using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Data
{
    [Table("Service")]
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Tittle { get; set; }
        [MaxLength(int.MaxValue)]
        public string? Description { get; set; }
        [MaxLength(int.MaxValue)]
        public string Image { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        public byte Isdelete { get; set; }
        public ICollection<Service_Bill> service_Bills { get; set; }
        public ICollection<Service_Cart> service_Carts { get; set; }
        public ICollection<Service_Detail> service_Details { get; set; }
        public Service()
        {
            service_Bills = new List<Service_Bill>();
            service_Carts = new List<Service_Cart>();
            service_Details = new List<Service_Detail>();
        }

    }
}
