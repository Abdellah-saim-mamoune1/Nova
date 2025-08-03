
namespace bankApI.BusinessLayer.Dto_s
{
    public class AccountGetDto
    {
        public int Id { get; set; }
        public string Account{ get; set; } = string.Empty;
        public double Balance { get; set; }
        public bool IsFrozen { get; set; }
        public DateOnly CreatedAt { get; set; }
        public CardGetDto? Card { get; set; }
    }
}
