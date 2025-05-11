using FluentValidation;
using Order.Application.Commands;

namespace Order.Application.Validators;

public class CheckoutOrderCommandValidator:AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("{UserName} is required")
            .NotNull()
            .MaximumLength(70)
            .WithMessage("{UserName} must not exceed 70 characters");

        RuleFor(x => x.TotalPrice)
            .NotEmpty()
            .WithMessage("{TotalPrice} is required")
            .GreaterThan(-1)
            .WithMessage("{TotalPrice} should not be -ve");
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("{EmailAddress is required}");
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .NotNull()
            .WithMessage("{FirstName is required}");
        RuleFor(x => x.LastName)
            .NotEmpty()
            .NotNull()
            .WithMessage("{LastName is required}");

    }
}
