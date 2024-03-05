using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Data
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } 
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
     
        public string Adddress { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Product_Cart> Product_Carts { get; set; }
        public virtual ICollection<Service_Cart> Service_Carts { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }


    }
}
