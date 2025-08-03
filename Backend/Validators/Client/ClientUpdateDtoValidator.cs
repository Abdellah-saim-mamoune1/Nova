using FluentValidation;
using bankApI.BusinessLayer.Dto_s;

public class ClientUpdateDtoValidator : AbstractValidator<UpdateClientDto>
{
    public ClientUpdateDtoValidator()
    {
       
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50);

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth date is required.")
            .Must(BeAtLeast18YearsOld).WithMessage("Client must be at least 18 years old.");

        RuleFor(x => x.personalEmail)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(200);
        RuleFor(p => p)
               .Must(p => p.Gender == "Male" || p.Gender == "Female")
             .NotEmpty().WithMessage("gender must be Male or Female");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.");
    }

    private bool BeAtLeast18YearsOld(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-age))
            age--;

        return age >= 18;
    }
}
