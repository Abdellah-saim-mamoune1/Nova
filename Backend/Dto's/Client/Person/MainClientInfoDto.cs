namespace bankApI.BusinessLayer.Dto_s.ClientDto_s.DClientMain
{
    public class MainClientInfoDto
    {

        public int PersonId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PersonalEmail { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public bool IsActive { get; set; }
        public string RoleType { get; set; } = null!;
        public int AccountId { get; set; }
        public string AccountAddress { get; set; } = string.Empty;
        public double Balance { get; set; }
        public bool IsFrozen { get; set; }
        public DateOnly CreatedAt { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public string ExpirationDate { get; set; } = string.Empty;


    }
}
