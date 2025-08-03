using bankApI.Models.ClientModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class TransferFundHistory
    {
        [Key]
        public int Id { get; set; }
        public int SenderAccountId { get; set; }
        public int RecipientAccountId { get; set; }
        public double Amount { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(SenderAccountId))]
        public Account? SenderAccountIds { get; set; }

        [ForeignKey(nameof(RecipientAccountId))]
        public Account? RecipientAccountIds { get; set; }
    }
}
