using bankApI.Models.EmployeeModels;

namespace bankApI.Dto_s.Employee
{
    public class BankRevenueGetPaginated
    {
       public required List <BankRevenue> BankRevenues { get; set; }
       public int TotalPages { get; set; }

    }


}
