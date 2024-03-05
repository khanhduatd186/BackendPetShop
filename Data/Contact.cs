using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Data
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(int.MaxValue)]
        public string? Website { get; set; }
        [MaxLength(int.MaxValue)]
        public string? Message { get; set; }
        public byte? IsRead { get; set; }
        public byte IsDelete { get; set; }
        public string IdUser { get; set; }
        [ForeignKey("IdUser")]
        public ApplicationUser applicationUser { get; set; }

    }
}
