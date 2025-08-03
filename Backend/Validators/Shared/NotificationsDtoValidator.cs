using FluentValidation;
using bankApI.BusinessLayer.Dto_s;

public class NotificationsDtoValidator : AbstractValidator<NotificationsDto>
{
    public NotificationsDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(31);

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Body is required.")
            .MaximumLength(501);

        RuleFor(x => x)
            .Must(x=>x.Type==1||x.Type==2||x.Type==3||x.Type==4||x.Type==6)
            .WithMessage("Invalid type ID.");

        RuleFor(x => x.Account)
            .NotEmpty().WithMessage("Account ID is required.");
    }
}
