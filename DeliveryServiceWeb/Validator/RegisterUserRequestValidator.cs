using DeliveryServiceWeb.Controller.User;
using FluentValidation;
namespace DeliveryServiceWeb.Validator;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
       
        RuleFor(x => x.PasswordHash)
            .NotEmpty()
            .WithMessage("Password is required");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches("[+]7[0-9]{10}")
            .WithMessage("Phone number is required");
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is required");
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(200)
            .WithMessage("Name is required");
        RuleFor(x => x.Surname)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(200)
            .WithMessage("Surname is required");
    
    }
}