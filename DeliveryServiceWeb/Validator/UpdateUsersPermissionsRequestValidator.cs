using DeliveryServiceWeb.Controller.User;
using FluentValidation;

namespace DeliveryServiceWeb.Validator;

public class UpdateUsersPermissionsRequestValidator : AbstractValidator<UpdateUsersPermissionsRequest>
{
    public UpdateUsersPermissionsRequestValidator()
    {
        RuleFor(x => x.Permissions)
            .NotEmpty()
            .Must(x => x.Count > 0 && x.Distinct().Count() == x.Count)
            .WithMessage("Permissions is required");
    }
}