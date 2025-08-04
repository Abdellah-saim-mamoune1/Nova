using bankApI.Models.ClientXEmployeeModels;
using bankApI.Models.EmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientModels
{
    public class Client
    {
        [Key, ForeignKey("Person")]
        public int PersonId { get; set; }
        public DateOnly CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public Person? Person { get; set; }
   
    }
}
