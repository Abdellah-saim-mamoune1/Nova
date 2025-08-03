namespace bankApI.BusinessLayer.Dto_s.ClientDto_s.DTransactionsHistory
{
    public class TransferFundSetDto
    {
        public string RecipientAccount { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string? Description { get; set; }

    }
}
