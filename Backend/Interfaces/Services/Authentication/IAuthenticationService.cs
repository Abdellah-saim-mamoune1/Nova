using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Dto_s.TokenDto_s;

namespace bankApI.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces
{
    public interface IAuthenticationService
    {
        public Task<ServiceResponseDto<TokenResponseDto?>> LoginAsync(LoginDto form);
        public Task<ServiceResponseDto<TokenResponseDto?>> RefreshTokensAsync(RefreshTokenRequestDto form);
    }
}
