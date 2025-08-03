using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Data;
using bankApI.Dto_s.Employee;
using bankApI.Interfaces.RepositoriesInterfaces.Employee;
using bankApI.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces;
using bankApI.Models.EmployeeModels;
using Microsoft.EntityFrameworkCore;

namespace bankApI.Services.EmployeeServices
{
    public class EmployeeManagementService
        (

         IEmployeeManagementRepository _EmployeeManagementRepository,

         AppDbContext Context

        ):IEmployeeManagementService
    {

        public async Task<ServiceResponseDto<EmployeeGetDto?>> GetEmployeeAsync(int Id)
        {
            var data=await _EmployeeManagementRepository.GetEmployeeAsync(Id);
            return new ServiceResponseDto<EmployeeGetDto?> { Data = data, Status = 200 };
        }

        public async Task<ServiceResponseDto<EmployeesPaginatedGetDto>> GetAllEmployeesAsync(int PageNumber, int PageSize)
        {
            var data = await _EmployeeManagementRepository.GetAllEmployeesAsync(PageNumber,PageSize);
            return new ServiceResponseDto<EmployeesPaginatedGetDto> { Data = data, Status = 200 };
        }


        public async Task<ServiceResponseDto<object?>> AddNewEmployeeAsync(EmployeePersonDto employee)
        {

            var response = await _EmployeeManagementRepository.AddNewEmployeeAsync(employee);

            if (!response)
                return new ServiceResponseDto<object?> { Status = 500 };
            
            return new ServiceResponseDto<object?> { Status = 200 };

        }


        public async Task<ServiceResponseDto<object?>> UpdateEmployeeAsync(EmployeeUpdateDto EmployeeInfo)
        {

            var EmployeeAccount = await ValidateAccountExistenceAsync(EmployeeInfo.Account);

            if (EmployeeAccount == null)
                return new ServiceResponseDto<object?> { Status = 400 };

            var response = await _EmployeeManagementRepository.UpdateEmployeeAsync(EmployeeAccount, EmployeeInfo);

            if (!response)
                return new ServiceResponseDto<object?> { Status = 500 };

            return new ServiceResponseDto<object?> { Status = 200 };

        }


        public async Task<ServiceResponseDto<object?>> SendEmployeeMessage(NotificationsDto Notification,int Id)
        {
            var data = await _EmployeeManagementRepository.SendEmployeeMessage(Notification,Id);

            if (!data)
                return new ServiceResponseDto<object?> { Status = 500 };

            return new ServiceResponseDto<object?> { Status = 200 };

        }


        public async Task<ServiceResponseDto<object?>> FreezeUnfreezeEmployeeAccountAsync(SetEmailStateDto state)
        {
            var EmployeeAccount = await ValidateAccountExistenceAsync(state.Account);

            if (EmployeeAccount == null)
                return new ServiceResponseDto<object?> { Status = 400 };

            var response = await _EmployeeManagementRepository.FreezeUnfreezeEmployeeAccountAsync(EmployeeAccount, state);

            if (!response)
                return new ServiceResponseDto<object?> { Status = 500 };

            return new ServiceResponseDto<object?> { Status = 200 };

        }

        public async Task<ServiceResponseDto<LoginRegisterHistoryGetPaginatedDto>> LoginRegisterGetPaginatedAsync(int PageNumber,int PageSize)
        {
            var data= await _EmployeeManagementRepository.GetLoginRegisterHistoryPaginatedAsync(PageNumber, PageSize);
            return new ServiceResponseDto<LoginRegisterHistoryGetPaginatedDto> { Status = 200,Data=data };

        }

        private async Task<EmployeeAccount?> ValidateAccountExistenceAsync(string Account)
        {
            return await Context.EmployeeAccount.Include(e => e.Person).FirstOrDefaultAsync(e => e.Account == Account);
        }



    }
}
