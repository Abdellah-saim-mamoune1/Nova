using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Dto_s.TokenDto_s;
using bankApI.Data;
using bankApI.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces;
using bankApI.Models.ClientModels;
using bankApI.Models.EmployeeModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bankApI.Services.AuthenticationServices
{
    public class AuthenticationValidationService(AppDbContext _db): IAuthenticationValidationService
    {
        public async Task<EmployeeAccount?> ValidateEmployeeLogin(LoginDto request)
        {
            var data = await ValidateEmployeeAccountXPassword(request);
            return data;
        }

        public async Task<Account?> ValidateClientLogin(LoginDto request)
        {
            var data = await ValidateClientAccountXPassword(request);
            return data;
        }

        public async Task<Account?> ValidateRefreshClientTokens(RefreshTokenRequestDto request)
        {
          
            var ClientAccount = await _db.Accounts.Include(a => a.token)
                .FirstOrDefaultAsync(t => t.token!.RefreshToken == request.RefreshToken);

            if (ClientAccount == null || ClientAccount.token!.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return null;

            return ClientAccount;

        }
        
            
         public async Task<EmployeeAccount?> ValidateRefreshEmployeeTokens(RefreshTokenRequestDto request)
         {
           
            var EmployeeAccount = await _db.EmployeeAccount.Include(t => t.Token)
                   .AsQueryable().FirstOrDefaultAsync(t => t.Token!.RefreshToken == request.RefreshToken);

            if (EmployeeAccount == null || EmployeeAccount.Token!.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return null;

            return EmployeeAccount;

        }

        private async Task<Account?> ValidateClientAccountXPassword(LoginDto request)
        {

            var ClientInfo = await _db.Accounts.Include(t => t.token)
                .FirstOrDefaultAsync(p => p.AccountAddress == request.Email);

            if (ClientInfo == null)
            {
                return null;
            }

            if (new PasswordHasher<Account>().VerifyHashedPassword(ClientInfo,
                ClientInfo.PassWord, request.Password)
           == PasswordVerificationResult.Failed)
            {

                return null;
            }

            return ClientInfo;

        }

        private async Task<EmployeeAccount?> ValidateEmployeeAccountXPassword(LoginDto request)
        {
                var employeeInfo = await _db.EmployeeAccount.Include(t => t.Token).
            Include(t => t.Person).ThenInclude(t => t!.Employee).ThenInclude(t => t!.EmployeeType)
            .FirstOrDefaultAsync(p => p.Account == request.Email);

                if (employeeInfo == null || employeeInfo?.Person?.Employee?.EmployeeType!.Type is null)
                {
                    return null;
                }

                if (new PasswordHasher<EmployeeAccount>().VerifyHashedPassword(employeeInfo,
                    employeeInfo.Password, request.Password)
               == PasswordVerificationResult.Failed)
                {

                    return null;
                }

                return employeeInfo;
        }
      
    }
}
