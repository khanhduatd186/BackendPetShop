using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Data
{
    [Table("Service_Cart")]
    public class Service_Cart
    {
        public int IdServie { get; set; }
        public string? IdUser { get; set; }
        public string Time { get; set; }
        public int Quantity { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Column(TypeName = "date")]
        public DateTime? dateTime { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public Service service { get; set; }
    }
}
