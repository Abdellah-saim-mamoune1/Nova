using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using FluentValidation;

namespace bankApI.Validators.Employee
{
    public class DepositWithdrawDtoValidator : AbstractValidator<DepositWithdrawDto>
    {
        public DepositWithdrawDtoValidator()
        {

            RuleFor(x => x.Amount)
                .GreaterThan(0).LessThanOrEqualTo(100000).WithMessage("Amount must be greater than 0 and less than or equal to 100000");
            RuleFor(x => x.EmployeeAccountId).GreaterThan(0).WithMessage("Id must be a positive number.");

            RuleFor(x => x.ClientAccount).MaximumLength(10).WithMessage("Account length must be less than 11.");

          
        }
    }
}
