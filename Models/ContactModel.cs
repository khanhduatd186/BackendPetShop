using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Models
{
 
    public class ContactModel
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


    }
}
