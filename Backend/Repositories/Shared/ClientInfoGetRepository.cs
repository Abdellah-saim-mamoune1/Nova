using bankApI.BusinessLayer.Dto_s;
using bankApI.Data;
using bankApI.Interfaces.Repositories.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace bankApI.Repositories.Shared
{
    public class ClientInfoGetRepository
        (
        AppDbContext Context,
        ILogger<ClientInfoGetRepository> _logger,
        IMemoryCache _cache
        
        ) : IClientInfoGetRepository
    {
        async public Task<GetClientInfoDto?> GetClientInfo(int Id)
        {

            if (_cache.TryGetValue($"Client_{Id}", out GetClientInfoDto? client))
                return client;
            try
            {
                client = (await Context.MainClientInfo
         .FromSqlInterpolated($"EXEC GetClientInfo {Id}").ToListAsync()).FirstOrDefault();


                _cache.Set<GetClientInfoDto?>($"Client_{Id}", client,TimeSpan.FromHours(10));
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching client info.");
                return null;
            }

            
        }
        async public Task<List<AccountGetDto>> GetClientAccounts(int Id)
        {
            return await Context.Accounts.Include(a => a.Card).AsQueryable().Where(a => a.PersonId == Id).Select(a => new AccountGetDto
            {
                Account = a.AccountAddress,
                Balance = a.Balance,
                CreatedAt = a.CreatedAt,
                Id = a.Id,
                IsFrozen = a.IsFrozen,
                Card = new CardGetDto { CardNumber = a.Card!.CardNumber, ExpirationDate = a.Card.ExpirationDate }
            }).ToListAsync();

        }

    }
}
