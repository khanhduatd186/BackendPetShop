using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Data
{
    [Table("Time")]
    public class Time
    {
        [Key]
        public int Id { get; set; }
        
        public string StartTime { get; set; }
      
     

        public ICollection<Service_Detail> service_Details { get; set; }
        public Time()
        {
            
            service_Details = new List<Service_Detail>();
        }
    }
}
