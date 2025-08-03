using bankApI.Models.ClientModels;
using bankApI.Models.EmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class Role
    {

        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public IEnumerable<Client> ? Clients { get; set; } 
        public IEnumerable<Employee>? Employees { get; set; }
    }
}
