using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Models
{
    public class SignInModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required]
        public string PassWord { get; set; } = null!;
    }
}
