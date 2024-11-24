using DeliveryServiceWeb.Controller.User;
using FluentValidation;

namespace DeliveryServiceWeb.Validator;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");

        RuleFor(x => x.PhoneNumber)
            .Matches("[+]7[0-9]{10}")
            .WithMessage("Phone number is required");
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email is required");
        RuleFor(x => x.Name)
            .MinimumLength(0)
            .MaximumLength(600)
            .WithMessage("Name is required");
        RuleFor(x => x.SurName)
            .MinimumLength(0)
            .MaximumLength(600)
            .WithMessage("Name is required")
            .WithMessage("Birth date is required");
    }
}