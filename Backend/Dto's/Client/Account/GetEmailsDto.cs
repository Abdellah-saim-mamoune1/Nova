
namespace bankApI.BusinessLayer.Dto_s
{
    public class GetEmailsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double Balance { get; set; }
        public bool IsFrozen { get; set; }
        public int PersonId { get; set; }
    }
}
