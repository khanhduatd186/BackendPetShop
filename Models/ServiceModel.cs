using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Models
{
    
    public class ServiceModel
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

      


    }
}
