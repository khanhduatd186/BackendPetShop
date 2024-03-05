using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Models
{
    public class ServiceIFModel
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Tittle { get; set; }
        [MaxLength(int.MaxValue)]
        public string? Description { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        public byte Isdelete { get; set; }

        public IFormFile Picture { get; set; }
    }
}
