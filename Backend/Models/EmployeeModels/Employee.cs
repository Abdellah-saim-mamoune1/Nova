using bankApI.Models.ClientXEmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.EmployeeModels
{
    public class Employee
    {
       
        [Key, ForeignKey("Person")]
        public int PersonId { get; set; }
        public double salary { get; set; }

        [ForeignKey("EmployeeType")]
        public int TypeId { get; set; }

        public bool IsActive { get; set; }      
        public DateOnly CreatedAt { get; set; }
        public Person? Person { get; set; }
        public EmployeeType? EmployeeType { get; set; }


    }
}
