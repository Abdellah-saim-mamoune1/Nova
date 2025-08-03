namespace bankApI.BusinessLayer.Dto_s.TokenDto_s
{
    public class RefreshTokenRequestDto
    {
        public required string RefreshToken { get; set; }
        public required string Role { get; set; }
    }
}
