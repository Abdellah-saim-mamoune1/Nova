using bankApI.Models.ClientXEmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientModels
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string AccountAddress { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;

        [ForeignKey("Person")]
        public int PersonId { get; set; }

        [ForeignKey("Card")]
        public int CardId { get; set; }

        [ForeignKey("Token")]
        public int TokenId { get; set; }

        [Required]
        public double Balance { get; set; }

        [Required]
        public bool IsFrozen { get; set; }

        [Required]
        public DateOnly CreatedAt { get; set; }
        public Person? Person { get; set; }
        public Card? Card { get; set; }
        public Token? token { get; set; }
        public IEnumerable<TransactionsHistory>? AccountTransactionHistory { get; set; }
        public IEnumerable<TransferFundHistory>? SenderAccount { get; set; }
        public IEnumerable<TransferFundHistory>? RecipientAccount { get; set; }
        public IEnumerable<ClientXNotifications>? clientXNotifications { get; set; }
        public IEnumerable<GetHelp>? GetHelp { get; set; }


    }

}
