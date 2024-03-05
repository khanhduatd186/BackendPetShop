using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Models
{
   
    public class TimeModel
    {
        [Key]
        public int Id { get; set; }
     
        public string StartTime { get; set; }
  
 
     
    }
}
