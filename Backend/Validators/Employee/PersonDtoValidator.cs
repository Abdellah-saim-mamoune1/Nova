using bankApI.BusinessLayer.Dto_s;
using FluentValidation;

namespace bankApI.Validators.Employee
{
    public class PersonDtoValidator : AbstractValidator<PersonDto>
    {
        public PersonDtoValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(20);

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(20);

            RuleFor(p => p.BirthDate)
                .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Birth date must be in the past");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("Address is required");

            RuleFor(p=>p)
                .Must(p=>p.Gender=="Male"||p.Gender=="Female")
              .NotEmpty().WithMessage("gender must be Male or Female");

            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\d{10,15}$").WithMessage("Phone number must be 10 to 15 digits");
        }
    }

      public class AccountDtoValidator : AbstractValidator<AccountDto>
      {
        public AccountDtoValidator()
        {
            RuleFor(a => a.PassWord)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");

            RuleFor(a => a.Balance)
                .GreaterThanOrEqualTo(0).WithMessage("Balance cannot be negative");
        }
    }

   
    public class PersonClientSetDtoValidator : AbstractValidator<PersonClientSetDto>
     {
        public PersonClientSetDtoValidator()
        {
            RuleFor(p => p.Person)
                .NotNull().WithMessage("Person is required")
                .SetValidator(new PersonDtoValidator());

            RuleFor(p => p.Account)
                .NotNull().WithMessage("Account is required")
                .SetValidator(new AccountDtoValidator());
        }
    }



}
