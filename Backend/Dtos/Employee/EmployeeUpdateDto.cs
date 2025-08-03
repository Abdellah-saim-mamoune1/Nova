namespace bankApI.BusinessLayer.Dto_s.EmployeeDto_s
{
    public class EmployeeUpdateDto:UpdateClientDto
    {
        public string Account { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
    }
}
