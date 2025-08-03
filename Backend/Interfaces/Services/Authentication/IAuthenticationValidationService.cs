using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Dto_s.TokenDto_s;
using bankApI.Models.ClientModels;
using bankApI.Models.EmployeeModels;

namespace bankApI.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces
{
    public interface IAuthenticationValidationService
    {
        public Task<EmployeeAccount?> ValidateEmployeeLogin(LoginDto request);
        public Task<Account?> ValidateClientLogin(LoginDto request);
        public Task<EmployeeAccount?> ValidateRefreshEmployeeTokens(RefreshTokenRequestDto request);
        public Task<Account?> ValidateRefreshClientTokens(RefreshTokenRequestDto request);

    }
}
