
namespace bankApI.BusinessLayer.Dto_s.EmployeeDto_s

{
    public class EmployeesPaginatedGetDto
    {
        public List<EmployeeGetDto>? Employees { get; set; }
        public int TotalPages { get; set; }

    }

    public class EmployeeGetDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }

        public string PersonalEmail { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public AccountDto accountInfo { get; set; } = new();

        public bool IsActive { get; set; }

    }
}
