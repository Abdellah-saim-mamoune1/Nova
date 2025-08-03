namespace bankApI.BusinessLayer.Dto_s.EmployeeDto_s
{
    public class DepositWithdrawDto
    {
        public string ClientAccount { get; set; } = string.Empty;
        public int EmployeeAccountId { get; set; }
        public double Amount { get; set; }
        public string ? Note { get; set; }
    }
}
