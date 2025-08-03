using bankApI.Dto_s.Employee;

namespace bankApI.BusinessLayer.Dto_s.EmployeeDto_s
{
    public class ClientsTransactionsHistoryPaginatedGetDto
    {
        public List<TransactionsHistoryGetDto>? Transactions { get; set; }
        public int TotalPages { get; set; }
    }


  

}
