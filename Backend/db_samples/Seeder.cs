
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Interfaces.Repositories.Shared;
using bankApI.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using bankApI.Interfaces.RepositoriesInterfaces.Employee;
using bankApI.Repositories.EmployeeRepositories;
using INotificationsRepository = bankApI.Interfaces.RepositoriesInterfaces.Employee.INotificationsRepository;

namespace bankApI.db_samples
{
    public class Seeder
    {

           IClientAdminRepository _clientManagementRepository;
           IEmployeeManagementRepository _employeeManagementRepository;
           INotificationsRepository _notificationRepository;
           bankApI.Interfaces.Repositories.Employee.ITransactionsManagementRepository _transactionsManagementRepository;
           IClientAdminRepository _clientAdminRepository;
           IClientInfoGetRepository _clientInfoGetRepository;
      
        public Seeder(
            IClientAdminRepository clientManagementRepository,
            IEmployeeManagementRepository employeeManagementRepository,
            INotificationsRepository notificationRepository,
            bankApI.Interfaces.Repositories.Employee.ITransactionsManagementRepository transactionsManagementRepository,
            IClientAdminRepository clientAdminRepository,
            IClientInfoGetRepository clientInfoGetRepository
                     ) 
        {

            _clientManagementRepository=clientManagementRepository;
            _employeeManagementRepository=employeeManagementRepository;
            _notificationRepository=notificationRepository;
            _transactionsManagementRepository = transactionsManagementRepository;
            _clientAdminRepository = clientAdminRepository;
            _clientInfoGetRepository = clientInfoGetRepository;
          
        }

        public async Task Seed()
        {
            foreach (var n in Sample.Notificationtypes) 
            { 
                await _notificationRepository.AddNotificationType(n);
            }

            foreach (var t in Sample.Employeetypes)
            {
                await _employeeManagementRepository.AddEmployeeTypeAsync(t);
            }

            foreach (var e in Sample.Employees)
            {
                await _employeeManagementRepository.AddNewEmployeeAsync(e);
            }

            foreach (var c in Sample.Clients)
            {
                await _clientManagementRepository.AddNewClientAsync(c);
            }

            foreach(var t in Sample.TransactionsTypes)
            {
                await _transactionsManagementRepository.AddTransactionsTypes(t);
            }

            var pag = await _clientAdminRepository.GetAllClientsAsync(1, 10);

            if (pag.Clients == null) return;

            foreach(var c in pag.Clients)
            {
                var accounts=await _clientInfoGetRepository.GetClientAccounts(c.Id);

                foreach (var e in accounts) {
                    await _transactionsManagementRepository.Deposit(new DepositWithdrawDto
                    {
                        Amount = 1000,
                        ClientAccount = e.Account,
                        EmployeeAccountId = 1,
                        Note = "No note"
                    });
                  }
            };
           
        }


    }
}
