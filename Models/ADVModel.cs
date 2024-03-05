using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ApiPetShop.Models
{
    public class ADVModel
    {

        public int Id { get; set; }

        [MaxLength(100)]
        public string Tittle { get; set; }
        [MaxLength(int.MaxValue)]
        public string? Description { get; set; }
        [MaxLength(int.MaxValue)]
        public string? Image { get; set; }
        [Required]
        public int Type { get; set; }
        [MaxLength(int.MaxValue)]
        [Required]
        public string Link { get; set; }
    }
}
