using System.ComponentModel.DataAnnotations;

namespace bankApI.Models.EmployeeModels
{
    public class BankRevenue
    {
            [Key]
            public int Id { get; set; }

            public double Amount { get; set; }

            public string Source { get; set; } = string.Empty;

            public DateTime Date { get; set; }

            public int RelatedTransactionId { get; set; }

            public string? Note { get; set; }

    }
}
