using System.ComponentModel.DataAnnotations;

namespace bankApI.Models.EmployeeModels
{
    public class EmployeeType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public IEnumerable<Employee>? Employees { get; set; }
    }
}
