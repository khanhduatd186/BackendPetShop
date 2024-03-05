using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Data
{
    [Table("Service_Detail")]
    public class Service_Detail
    {
        
        public int IdService { get; set; }
        public int IdTime { get; set; }
        public int SoLuongCa { get; set; }
        public DateTime NgayThucHien { get; set; }
        public Time time { get; set; }
        public Service service { get; set; }
    }
}
