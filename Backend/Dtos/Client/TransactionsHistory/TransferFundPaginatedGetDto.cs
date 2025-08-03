namespace bankApI.Dto_s.Client.TransactionsHistory
{
    public class TransferFundPaginatedGetDto
    {
        public List<TransferFundGetDto>? Transfers { get; set; }
        public int TotalPages { get; set; }
    }


    public class TransferFundGetDto
    {
        public int Id { get; set; }
        public string SenderAccount { get; set; } = string.Empty;
        public string RecipientAccount { get; set; } = string.Empty;
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
