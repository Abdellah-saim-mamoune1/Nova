
namespace bankApI.Dto_s.Employee
{

    public class LoginRegisterHistoryGetPaginatedDto
    {
       public List<LoginRegisterHistoryGetDto>? LoginRegisterHistory { get; set; }

       public int TotalPages { get; set; }
    }

    public class LoginRegisterHistoryGetDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
    }


}
