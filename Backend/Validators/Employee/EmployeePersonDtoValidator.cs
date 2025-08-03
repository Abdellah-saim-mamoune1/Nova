using bankApI.BusinessLayer.Dto_s;
using bankApI.Models.EmployeeModels;
using FluentValidation;

namespace bankApI.Validators.Employee
{
    public class EmployeePersonDtoValidator : AbstractValidator<EmployeePersonDto>
    {

        public EmployeePersonDtoValidator()
        {
            RuleFor(p => p.Person)
                    .NotNull().WithMessage("Person is required")
                    .SetValidator(new PersonDtoValidator());

                RuleFor(p => p.EmployeeAccount)
                    .NotNull().WithMessage("Account is required")
                    .SetValidator(new EmployeeAccountDtoValidator());

                RuleFor(p => p.Employee)
                   .NotNull().WithMessage("Account is required")
                   .SetValidator(new EmployeeDtoValidator());
            
        }
    }

        public class EmployeeAccountDtoValidator: AbstractValidator<EmployeeAccountDto>
        {
            public EmployeeAccountDtoValidator()
        {
            RuleFor(p=>p.Password).NotEmpty().MaximumLength(10).MinimumLength(8).WithMessage("Password length must be between 8 and 10.");
        }

    }
        public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
        {
            public EmployeeDtoValidator()
            {
               

                RuleFor(x => x.TypeId)
                    .GreaterThan(0).WithMessage("TypeId must be a positive number");
            }
        }


    
}
