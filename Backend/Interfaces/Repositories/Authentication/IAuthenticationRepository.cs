using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.Models.ClientModels;
using bankApI.Models.EmployeeModels;

namespace bankApI.Interfaces.RepositoriesInterfaces.AuthenticationRepositoryInterfaces
{
    public interface IAuthenticationRepository
    {
        public Task<TokenResponseDto?> CreateEmployeeTokensAsync(LoginDto login, EmployeeAccount AccountInfo);
        public Task<TokenResponseDto?> CreateClientTokensAsync(LoginDto login, Account AccountInfo);
        public Task<TokenResponseDto?> RefreshEmployeeTokensAsync(EmployeeAccount AccountInfo);
        public Task<TokenResponseDto?> RefreshClientTokensAsync(Account AccountInfo);
    }
}
