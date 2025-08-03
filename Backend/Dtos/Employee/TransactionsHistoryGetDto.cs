namespace bankApI.Dto_s.Employee
{
    public class TransactionsHistoryGetDto: bankApI.BusinessLayer.Dto_s.Client.TransactionsHistoryDto.TransactionsHistoryGetDto
    {
        public int AccountId { get; set; }
        public string Account { get; set; } = string.Empty;
    }
}
