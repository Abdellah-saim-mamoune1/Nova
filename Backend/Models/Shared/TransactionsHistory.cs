using bankApI.Models.ClientModels;
using bankApI.Models.EmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class TransactionsHistory
    {
        [Key]
        public  int Id { get; set; }

        [ForeignKey("Account")]
        public int  AccountId { get; set; }

        [ForeignKey("EmployeeAccount")]
        public int EmployeeAccountId { get; set; }

        [ForeignKey("TransactionsType")]
        public int TypeId { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; } 
        public TransactionsType? TransactionsType { get; set; }
        public Account? Account { get; set; }
        public EmployeeAccount? EmployeeAccount { get; set; }


    }
}
