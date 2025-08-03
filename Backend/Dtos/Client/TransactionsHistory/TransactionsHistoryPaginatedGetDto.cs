namespace bankApI.BusinessLayer.Dto_s.Client.TransactionsHistoryDto
{
    public class TransactionsHistoryPaginatedGetDto
    {
        public List<TransactionsHistoryGetDto>? Transactions { get; set; }
        public int TotalPages { get; set; }
    }


    public class TransactionsHistoryGetDto
    {

        public int Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public double Amount { get; set; }

        public DateTime CreatedAt { get; set; }
    }


}
