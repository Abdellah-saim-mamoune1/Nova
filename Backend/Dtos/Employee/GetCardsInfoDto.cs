namespace bankApI.BusinessLayer.Dto_s.EmployeeDto_s
{
    public class GetCardsInfoDto
    {
        public int totalClients { get; set; }
        public int totalStaff { get; set; }
        public short newClients { get; set; }
        public double totalWithdraws { get; set; }
        public double totalDeposits { get; set; }
        public double totalTransfers { get; set; }
    }
}
