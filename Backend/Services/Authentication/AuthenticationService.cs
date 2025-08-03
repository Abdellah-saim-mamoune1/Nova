using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Dto_s.TokenDto_s;
using bankApI.Interfaces.RepositoriesInterfaces.AuthenticationRepositoryInterfaces;
using bankApI.Interfaces.RepositoriesInterfaces.Employee;
using bankApI.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces;


namespace bankApI.Services.AuthenticationServices
{
    public class AuthenticationService
        (
        IAuthenticationValidationService _Validate,
        IAuthenticationRepository _AuthenticationRepository,
        IEmployeeManagementRepository _EmployeeManagementRepository
        ) : IAuthenticationService
    {

        public async Task<ServiceResponseDto<TokenResponseDto?>> LoginAsync(LoginDto form)
        {
            if (form.Email.EndsWith("@Nova.com")) 
                return await LoginEmployeeAsync(form);

            return await LoginClientAsync(form);

        }

        private async Task<ServiceResponseDto<TokenResponseDto?>> LoginEmployeeAsync(LoginDto form)
        {
            var validate = await _Validate.ValidateEmployeeLogin(form);
            if(validate==null)
                return new ServiceResponseDto<TokenResponseDto?> { Status=400 };

            var data = await _AuthenticationRepository.CreateEmployeeTokensAsync(form,validate);

            if (data==null)
                return new ServiceResponseDto<TokenResponseDto?> { Status = 500 };

             await _EmployeeManagementRepository.LoginRegisterAsync(form.Email);
             return new ServiceResponseDto<TokenResponseDto?> {Data=data, Status = 200 };
             
        }

        private async Task<ServiceResponseDto<TokenResponseDto?>> LoginClientAsync(LoginDto form)
        {
            var validate = await _Validate.ValidateClientLogin(form);
            if (validate == null)
                return new ServiceResponseDto<TokenResponseDto?> { Status = 400 };

            var data = await _AuthenticationRepository.CreateClientTokensAsync(form, validate);

            if (data == null)
                return new ServiceResponseDto<TokenResponseDto?> { Status = 500 };

            return new ServiceResponseDto<TokenResponseDto?> { Data = data, Status = 200 };

        }

        public async Task<ServiceResponseDto<TokenResponseDto?>> RefreshTokensAsync(RefreshTokenRequestDto form)
        {
            if (form.Role == "Client")
                return await RefreshClientTokensAsync(form);

            return await RefreshEmployeeTokensAsync(form);

        }

        private async Task<ServiceResponseDto<TokenResponseDto?>> RefreshClientTokensAsync(RefreshTokenRequestDto form)
        {
            var validate = await _Validate.ValidateRefreshClientTokens(form);
            if (validate == null)
                return new ServiceResponseDto<TokenResponseDto?> { Status = 400 };

            var data = await _AuthenticationRepository.RefreshClientTokensAsync(validate);

            if (data == null)
                return new ServiceResponseDto<TokenResponseDto?> { Status = 500 };

            return new ServiceResponseDto<TokenResponseDto?> { Data = data, Status = 200 };

        }

        private async Task<ServiceResponseDto<TokenResponseDto?>> RefreshEmployeeTokensAsync(RefreshTokenRequestDto form)
        {
            var validate = await _Validate.ValidateRefreshClientTokens(form);
            if (validate == null)
                return new ServiceResponseDto<TokenResponseDto?> { Status = 400 };

            var data = await _AuthenticationRepository.RefreshClientTokensAsync(validate);

            if (data == null)
                return new ServiceResponseDto<TokenResponseDto?> { Status = 500 };

            return new ServiceResponseDto<TokenResponseDto?> { Data = data, Status = 200 };

        }

    }
}
