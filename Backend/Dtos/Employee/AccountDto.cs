namespace bankApI.BusinessLayer.Dto_s.EmployeeDto_s
{
    public class AccountDto
    {

        public string Account { get; set; } = string.Empty;
        public bool IsFrozen { get; set; }
        public DateOnly CreatedAt { get; set; }


    }
}
