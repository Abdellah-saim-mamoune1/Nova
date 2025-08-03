using bankApI.BusinessLayer.Dto_s;
using FluentValidation;

namespace bankApI.Validators.Employee
{
    public class SetEmailStateDtoValidator : AbstractValidator<SetEmailStateDto>
    {
        public SetEmailStateDtoValidator()
        {
            RuleFor(x => x.Account)
                .MaximumLength(51)
                .NotEmpty().WithMessage("Account is required")
                .WithMessage("Account must be 10 to 30 digits");

            RuleFor(x => x.state)
                .NotEmpty().WithMessage("State is required")
                .Must(s => s == "DisActivate" || s == "Activate")
                .WithMessage("State must be either 'enabled' or 'disabled'");
        }
    }
}
