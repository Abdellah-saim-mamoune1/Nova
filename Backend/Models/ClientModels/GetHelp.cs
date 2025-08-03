using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientModels
{
    public class GetHelp
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Account")]
        public int ClientAccountId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public Account? Account { get; set; }
    }
}
