using bankApI.BusinessLayer.Dto_s;
using bankApI.Data;
using bankApI.Dto_s.Employee;
using bankApI.Interfaces.RepositoriesInterfaces.Employee;
using bankApI.Methods;
using bankApI.Models.ClientModels;
using bankApI.Models.ClientXEmployeeModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bankApI.Repositories.EmployeeRepositories
{
    public class ClientAdminRepository(AppDbContext _db,ILogger<ClientAdminRepository> _logger) : IClientAdminRepository
    {
        public async Task<string?> AddNewClientAsync(PersonClientSetDto Client)
        {
            string? Account=null;
            try
            {

                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    int PersonId = await AddClientPerson(Client.Person);
                    int CardId = await AddCard();
                    int TokenId = await AddToken();
                    int AccountId = await AddAccount(PersonId, Client, TokenId, CardId);

                    var client = new bankApI.Models.ClientModels.Client
                    {
                        PersonId = PersonId,
                        IsActive = false
                    };

                    await _db.Clients.AddAsync(client);
                    await _db.SaveChangesAsync();
                    var account = await _db.Accounts.AsQueryable().FirstAsync(a => a.Id == AccountId);
                    Account = account.AccountAddress;
                    await transaction.CommitAsync();
                    

                });
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding new client.");
               
            }
            return Account;

        }

        public async Task<GetPaginatedClientsInfoDto> GetAllClientsAsync(int PageNumber,int PageSize)
        {
            var AllClients = _db.Clients
           .Include(c => c.Person)
         
           .AsQueryable();

           var clients=await AllClients
       .Select(c => new GetClientInfoDto
       {
           Id = c.PersonId,
           FirstName = c.Person!.FirstName,
           LastName = c.Person.LastName,
           PersonalEmail = c.Person.Email,
           Address = c.Person.Address,
           BirthDate = c.Person.BirthDate,
           PhoneNumber = c.Person.PhoneNumber,

           IsActive = c.IsActive
       }).Skip((PageNumber-1)*PageSize).Take(PageSize)
       .ToListAsync();


            return new GetPaginatedClientsInfoDto {TotalPages= (int)Math.Ceiling((float)AllClients.Count() / PageSize), Clients=clients };
        }    

        public async Task<AccountGetDto?> AddNewAccountAsync(BankEmailDto email)
        {

            try
            {
                AccountGetDto info = new ();
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var Account = await _db.Accounts.AsQueryable().Where(A => A.AccountAddress == email.CurrentAccount).FirstAsync();
                    int CardId = await AddCard();
                    int TokenId = await AddToken();
           
            var hashedPassword = new PasswordHasher<BankEmailDto>()
            .HashPassword(email, email.PassWord);

            Account account = new Account
            {
                PersonId = Account.PersonId,
                AccountAddress = GenerateKeys.GenerateId(10),
                PassWord = hashedPassword,
                Balance = email.InitialBalance,
                IsFrozen = false,
                CardId = CardId,
                TokenId = TokenId
            };

            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();
            DateOnly date = new DateOnly();
            info = new AccountGetDto
            {
                Account = account.AccountAddress,
                Balance = account.Balance,
                IsFrozen = false,
                CreatedAt = date,
               
            };

            Notification Notification = new Notification
            {
                Body = "The new account is " + account.AccountAddress + " with the password: " + account.PassWord,
                Title = "New account",
                TypeId = 1

            };

            await _db.Notifications.AddAsync(Notification);
            await _db.SaveChangesAsync();
            ClientXNotifications notification = new ClientXNotifications
            {
                AccountId = Account.PersonId,
                NotificationId = Notification.Id,
                IsViewed = false


             };

                    await _db.ClientXNotifications.AddAsync(notification);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                });
                return info;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding new client account.");
                return null;
            }

        }
       
        public async Task<bool> FreezeClientAccountAsync(SetEmailStateDto state)
        {
            try
            {
                var c = await _db.Accounts.FirstAsync(d => d.AccountAddress == state.Account);

                if (state.state == "DisActivate")
                    c.IsFrozen = true;
                else
                    c.IsFrozen = false;
                await _db.SaveChangesAsync();

                return true;
            } catch(Exception ex)
            {
                _logger.LogError(ex, "Error while freezing client account.");
                return false;
            }
        }

        public async Task<bool> SendClientAccountMessage(NotificationsDto Notification)
        {
            try
            {

                var account = await _db.Accounts.FirstAsync(c => c.AccountAddress == Notification.Account);

                var type = await _db.NotificationsTypes.FirstOrDefaultAsync(c => c.Id == Notification.Type);

                Notification notification = new Notification { Title = Notification.Title, Body = Notification.Body, TypeId = Notification.Type };
                await _db.Notifications.AddAsync(notification);
                await _db.SaveChangesAsync();

                ClientXNotifications notify = new ClientXNotifications { AccountId = account.Id, NotificationId = notification.Id, IsViewed = false };

                await _db.ClientXNotifications.AddAsync(notify);
                await _db.SaveChangesAsync();
                return true;
            }catch(Exception ex)
            {

                _logger.LogError(ex, "Error while sending message to client account.");
                return false;
            }
        }
   
        private async Task<int> AddClientPerson(PersonDto Person)
        {
            Person person = new Person
            {
                FirstName = Person.FirstName,
                LastName = Person.LastName,
                BirthDate = Person.BirthDate,
                PhoneNumber = Person.PhoneNumber,
                Address = Person.Address,
                Email = Person.Email,
                Gender=Person.Gender
            };
            await _db.Persons.AddAsync(person);
            await _db.SaveChangesAsync();

            return person.Id;
        }

        private async Task<int> AddCard()
        {

            Random random = new Random();
            int cvv = random.Next(100, 1000);

            Card card = new Card
            {
                CardNumber = GenerateKeys.GenerateNumberId(16),
                CVV = cvv,
                ExpirationDate = DateTime.UtcNow.AddYears(1).ToString("MM/yy")

            };

            await _db.Cards.AddAsync(card);
            await _db.SaveChangesAsync();

            return card.Id;
        }

        private async Task<int> AddToken()
        {

            Token token = new Token
            {
                RefreshToken = GenerateKeys.GenerateId(10),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
            };

            await _db.Tokens.AddAsync(token);
            await _db.SaveChangesAsync();

            return token.Id;
        }

        private async Task<int> AddAccount(int PersonId, PersonClientSetDto Client, int TokenId, int CardId)
        {
            var hashedPassword = new PasswordHasher<PersonClientSetDto>()
             .HashPassword(Client, Client.Account.PassWord);

            Account account = new Account
            {
                PersonId = PersonId,
                AccountAddress = GenerateKeys.GenerateId(10),
                PassWord = hashedPassword,
                Balance = Client.Account.Balance,
                IsFrozen = false,
                CardId = CardId,
                TokenId = TokenId,

            };
            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();

            return account.Id;
        }


    }
}
