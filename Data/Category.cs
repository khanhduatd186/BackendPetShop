using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Data
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(int.MaxValue)]
        public string Tittle { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
