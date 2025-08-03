using System.ComponentModel.DataAnnotations;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class TransactionsType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public IEnumerable<TransactionsHistory>? TransactionHistory { get; set; }
    }
}
