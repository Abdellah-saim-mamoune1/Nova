using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Data;
using bankApI.Dto_s.Employee;
using bankApI.Interfaces.RepositoriesInterfaces.Employee;
using bankApI.Methods;
using bankApI.Models.ClientModels;
using bankApI.Models.ClientXEmployeeModels;
using bankApI.Models.EmployeeModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace bankApI.Repositories.EmployeeRepositories
{
    public class EmployeeManagementRepository
        (
        AppDbContext _db,
        ILogger<EmployeeManagementRepository> _logger,
        IMemoryCache _cache
        
        ) : IEmployeeManagementRepository
    {

        public async Task<bool> AddNewEmployeeAsync(EmployeePersonDto employee)
        {
            try
            {

                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    int PersonId = await AddEmployeePersonAsync(employee.Person);
                    await AddEmployeeInfoAsync(employee.Employee, PersonId);
                    int TokenId = await AddTokenAsync();
                    await AddAccountAsync(employee, PersonId, TokenId);

                    await transaction.CommitAsync();

                });
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding new employee.");
                return false;
            }

        }

        async public Task<EmployeeGetDto?> GetEmployeeAsync(int Id)
        {
            if (_cache.TryGetValue($"Employee_{Id}", out EmployeeGetDto? employee))
                return employee;

               employee = await _db.EmployeeAccount.AsQueryable().Include(C => C.Person).ThenInclude(C => C!.Employee).ThenInclude(C => C!.EmployeeType).
                Where(C => C.EmployeeId == Id).Select(c => new EmployeeGetDto
                {
                    FirstName = c.Person!.FirstName,
                    LastName = c.Person.LastName,
                    accountInfo =
                   new BusinessLayer.Dto_s.EmployeeDto_s.AccountDto
                   {
                       Account = c.Person!.EmployeeAccount!.Account,
                       Salary = c.Person!.Employee!.salary,
                       IsFrozen = c.Person.EmployeeAccount.IsFrozen,
                       CreatedAt = c.Person.Employee.CreatedAt

                   },
                    PersonalEmail = c.Person.Email,
                    Address = c.Person.Address,
                    BirthDate = c.Person.BirthDate,
                    PhoneNumber = c.Person.PhoneNumber,
                    Type = c.Person.Employee.EmployeeType!.Type,
                    RoleType = c.Person.Employee.Role!.Type,
                    IsActive = c.Person.Employee.IsActive

                }).FirstOrDefaultAsync();
            _cache.Set<EmployeeGetDto?>($"Employee_{Id}", employee, TimeSpan.FromHours(10));

            return employee;
        }

        public async Task<EmployeesPaginatedGetDto> GetAllEmployeesAsync(int PageNumber, int PageSize)
        {
            var AllEmployees = _db.EmployeeAccount.Include(C => C.Person).ThenInclude(C => C!.Employee).ThenInclude(C => C!.EmployeeType).AsQueryable();
            var employees =await AllEmployees
          .Select(c => new EmployeeGetDto
          {
              FirstName = c.Person!.FirstName,
              LastName = c.Person.LastName,
              accountInfo =
                new BusinessLayer.Dto_s.EmployeeDto_s.AccountDto
                {
                    Account = c.Person!.EmployeeAccount!.Account,
                    Salary = c.Person.Employee!.salary,
                    IsFrozen = c.Person.EmployeeAccount.IsFrozen,
                    CreatedAt = c.Person.Employee.CreatedAt
                },

              PersonalEmail = c.Person.Email,
              Address = c.Person.Address,
              BirthDate = c.Person.BirthDate,
              PhoneNumber = c.Person.PhoneNumber,
              Type = c.Person.Employee.EmployeeType!.Type,
              RoleType = c.Person.Employee.Role!.Type,
              IsActive = c.Person.Employee.IsActive

          }).Skip((PageNumber-1)*PageSize)
          .Take(PageSize)
          .ToListAsync();

            return new EmployeesPaginatedGetDto {Employees=employees,TotalPages = (int)Math.Ceiling((float)AllEmployees.Count() / PageSize)};
        }

        public async Task<bool> UpdateEmployeeAsync(EmployeeAccount Employee, EmployeeUpdateDto EmployeeInfo)
        {
            try
            {

                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var Type = await _db.EmployeeType.AsQueryable().FirstAsync(c => c.Type == EmployeeInfo.type);

                    Employee.Person!.FirstName = EmployeeInfo.FirstName ;
                    Employee.Person.LastName = EmployeeInfo.LastName ;
                    Employee.Person.PhoneNumber = EmployeeInfo.PhoneNumber;
                    Employee.Person.BirthDate = EmployeeInfo.BirthDate;
                    Employee.Person.Address = EmployeeInfo.Address;
                    Employee.Person.Email = EmployeeInfo.personalEmail;
                    Employee.Person.Employee!.TypeId = Type.Id;
                    Employee.Person.Gender = EmployeeInfo.Gender;
                    await _db.SaveChangesAsync();

                    await transaction.CommitAsync();

                });

                _cache.Remove($"Employee_{Employee.Id}");
                 return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating employee.");
                return false;
            }


        }

        public async Task<bool> FreezeUnfreezeEmployeeAccountAsync(EmployeeAccount Account, SetEmailStateDto state)
        {
            try
            {
              
                if (state.state == "DisActivate")
                    Account.IsFrozen = true;
                else
                    Account.IsFrozen = false;

                await _db.SaveChangesAsync();
                return true;

            } catch(Exception ex)
            {
                _logger.LogError(ex, "Error while afreezing employee account.");
                return false;
            }


        }

        public async Task<bool> SendEmployeeMessage(NotificationsDto Notification, int Id)
        {

            try
            {

                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var account = await _db.EmployeeAccount.FirstAsync(c => c.EmployeeId ==Id);
                    var type = await _db.NotificationsTypes.FirstAsync(c => c.Id == Notification.Type);

                    Notification notification = new Notification
                    {
                        Title = Notification.Title,
                        Body = "from " + account.Account + ": " +
                        Notification.Body,
                        TypeId = Notification.Type
                    };
                    await _db.Notifications.AddAsync(notification);
                    await _db.SaveChangesAsync();

                    EmployeeNotifications notify = new EmployeeNotifications { AccountId = account!.Id, NotificationId = notification.Id, Isviewed = false };

                    await _db.EmployeeNotifications.AddAsync(notify);
                    await _db.SaveChangesAsync();
                });

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while sending message to employee.");
                return false;
            }

        }

        private async Task<int> AddEmployeePersonAsync(PersonDto person)
        {
            var Person = new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                BirthDate = person.BirthDate,
                Address = person.Address,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber,
                Gender=person.Gender
            };

            await _db.Persons.AddAsync(Person);
            await _db.SaveChangesAsync();

            return Person.Id;
        }

        private async Task AddEmployeeInfoAsync(EmployeeDto employee, int PersonId)
        {
            var Employee = new Models.EmployeeModels.Employee
            { 
                PersonId = PersonId,
                RoleTypeId = employee.TypeId,
                TypeId = employee.TypeId,
                IsActive = false,
               
            };

            await _db.Employees.AddAsync(Employee);
            await _db.SaveChangesAsync();
        }

        private async Task<int> AddTokenAsync()
        {
            Token token = new Token
            {
                RefreshToken = GenerateKeys.GenerateId(32),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)

            };

            await _db.Tokens.AddAsync(token);
            await _db.SaveChangesAsync();

            return token.Id;
        }

        private async Task AddAccountAsync(EmployeePersonDto employee, int PersonId, int TokenId)
        {
           
                    var hashedPassword = new PasswordHasher<EmployeePersonDto>()
             .HashPassword(employee, employee.EmployeeAccount.Password);

                    var empAcc = new EmployeeAccount
                    {
                        EmployeeId = PersonId,
                        Account = employee.Person.FirstName.Trim().ToLower() + "." + employee.Person.LastName.Trim().ToLower() + "@Nova.com",
                        Password = hashedPassword,
                        IsFrozen = false,
                        TokenId = TokenId
                    };

                    await _db.EmployeeAccount.AddAsync(empAcc);
                    await _db.SaveChangesAsync();
                  
        }

        public async Task LoginRegisterAsync(string EmployeeAccount)
        {
            var Account = await _db.EmployeeAccount.AsQueryable().FirstAsync(a => a.Account == EmployeeAccount);
            var register = new LoginRegister
            {
                EmployeeId = Account.EmployeeId
            };

             _db.Add(register);
            await _db.SaveChangesAsync();   
        }

        public async Task<LoginRegisterHistoryGetPaginatedDto> GetLoginRegisterHistoryPaginatedAsync(int PageNumber,int PageSize)
        {
            var AllLoginRegister = _db.LoginRegister.AsQueryable();
            var Register=await AllLoginRegister.Select(l => new LoginRegisterHistoryGetDto
            {
                   Id=l.Id,
                   Date=l.Date,
                   EmployeeId=l.EmployeeId

            }).Skip((PageNumber-1)*PageSize).Take(PageSize).ToListAsync();

           
            var PaginatedRegister = new LoginRegisterHistoryGetPaginatedDto { LoginRegisterHistory = Register, 
                TotalPages =(int) Math.Ceiling((float)AllLoginRegister.Count()/PageSize) };

            return PaginatedRegister;
        }

    }
}
