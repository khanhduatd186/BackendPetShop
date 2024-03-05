using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Data
{
    [Table("Service_Bill")]
    public class Service_Bill
    {
        public int IdService { get; set; }
        public int IdBill { get; set; }
        [Range(0, 100)]
        public int Quantity { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public Service service { get; set; }
        public Bill bill { get; set; }

    }
}
