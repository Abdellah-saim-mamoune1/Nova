using bankApI.BusinessLayer.Dto_s.ClientDto_s.DTransactionsHistory;
using FluentValidation;

namespace bankApI.Validators.Client
{
    public class TransferFundSetDtoValidator : AbstractValidator<TransferFundSetDto>
    {
        public TransferFundSetDtoValidator()
        {
           

            RuleFor(x => x.RecipientAccount)
                .NotEmpty().WithMessage("Recipient account is required")
                .MinimumLength(8).MaximumLength(15).WithMessage("Recipient account length must be 8 to 15");

            RuleFor(x => x.Amount)
                .GreaterThan(0).LessThanOrEqualTo(1000000).WithMessage("Amount must be greater than 0 and less than or equal to 1000000");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
