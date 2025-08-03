using bankApI.BusinessLayer.Dto_s;
using FluentValidation;

namespace bankApI.Validators.Employee 
{
    public class BankEmailDtoValidator : AbstractValidator<BankEmailDto>
    {
        public BankEmailDtoValidator()
        {
            RuleFor(b => b.CurrentAccount)
                .NotEmpty().WithMessage("Current account is required")
                .WithMessage("Account must be 10 to 20 digits");

            RuleFor(b => b.PassWord)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(b => b.InitialBalance)
                .GreaterThanOrEqualTo(0).WithMessage("Initial balance cannot be negative");
        }
    }
}