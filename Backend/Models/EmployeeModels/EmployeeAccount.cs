using bankApI.Models.ClientModels;
using bankApI.Models.ClientXEmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.EmployeeModels
{
    public class EmployeeAccount
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Person")]
        public  int EmployeeId { get; set; }
        [Required]
        public string Account { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool IsFrozen { get; set; }

        [ForeignKey("Token")]
        public int TokenId { get; set; }
        public Person? Person { get; set; }
        public Token? Token { get; set; }
        public IEnumerable<EmployeeNotifications>? EmployeeNotifications { get; set; }
        public IEnumerable<TransactionsHistory>? TransactionsHistory { get; set; }
        
    }
}
