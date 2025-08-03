using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.Data;
using bankApI.Interfaces.RepositoriesInterfaces.AuthenticationRepositoryInterfaces;
using bankApI.Methods;
using bankApI.Models.ClientModels;
using bankApI.Models.EmployeeModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bankApI.Repositories.AuthenticationRepositories
{
    public class AuthenticationRepository
        (
        AppDbContext Context
        ,IConfiguration Configuration
        ,ILogger<AuthenticationRepository> _logger
        ) : IAuthenticationRepository
    {


        public async Task<TokenResponseDto?> CreateEmployeeTokensAsync(LoginDto login,EmployeeAccount AccountInfo)
        {
            try
            {

                TokenResponseDto token = CreateEmployeeTokenResponse(AccountInfo.EmployeeId, AccountInfo.Person!.Employee!.EmployeeType!.Type);
                AccountInfo.Token!.RefreshToken = token.RefreshToken;
                AccountInfo.Token.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await Context.SaveChangesAsync();

                return token;
            } catch(Exception ex)
            {
                _logger.LogError(ex, "Error while creating employee tokens.");
                return null;
            }

        }

        public async Task<TokenResponseDto?> CreateClientTokensAsync(LoginDto login,Account AccountInfo)
        {

            try
            {

                TokenResponseDto token = CreateClientTokenResponse(AccountInfo.PersonId,AccountInfo.AccountAddress, "Client");
                AccountInfo.token!.RefreshToken = token.RefreshToken;
                AccountInfo.token.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await Context.SaveChangesAsync();
                return token;

            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating client tokens.");
                return null;
            }
        }

        public async Task<TokenResponseDto?> RefreshEmployeeTokensAsync(EmployeeAccount AccountInfo)
        {

            try
            {
                var token = CreateEmployeeTokenResponse(AccountInfo.EmployeeId, AccountInfo.Person!.Employee!.EmployeeType!.Type);
                AccountInfo.Token!.RefreshToken = token.RefreshToken;
                AccountInfo.Token.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await Context.SaveChangesAsync();
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while refreshing employee tokens.");
                return null;
            }
        }

        public async Task<TokenResponseDto?> RefreshClientTokensAsync(Account AccountInfo)
        {
            try
            {
                var token = CreateClientTokenResponse(AccountInfo.PersonId,AccountInfo.AccountAddress, "Client");
                AccountInfo.token!.RefreshToken = token.RefreshToken;
                AccountInfo.token.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await Context.SaveChangesAsync();

                return token;
            } catch(Exception ex)
            {
                _logger.LogError(ex, "Error while refreshing client tokens.");
                return null;
            }
        }

        private TokenResponseDto CreateClientTokenResponse(int Id,string Account, string Role)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateClientToken(Id,Account, Role),
                RefreshToken = GenerateRefreshToken()
            };
        }
        private TokenResponseDto CreateEmployeeTokenResponse(int Id, string Role)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateEmployeeToken(Id, Role),
                RefreshToken = GenerateRefreshToken()
            };
        }

        private static string GenerateRefreshToken()
        {
            return GenerateKeys.GenerateId(32);
        }

        private string CreateClientToken(int Id,string Account, string Role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,Id.ToString()),
                new Claim(ClaimTypes.Email,Account),
                new Claim(ClaimTypes.Role, Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: Configuration.GetValue<string>("AppSettings:Issuer"),
                audience: Configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1000),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private string CreateEmployeeToken(int Id, string Role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,Id.ToString()),
                new Claim(ClaimTypes.Role, Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: Configuration.GetValue<string>("AppSettings:Issuer"),
                audience: Configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }



    }
}
