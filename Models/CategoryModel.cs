using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Models
{
    public class CategoryModel
    { 
        public int Id { get; set; }
        [Required]
        [MaxLength(int.MaxValue)]
        public string Tittle { get; set; }
    }
}
