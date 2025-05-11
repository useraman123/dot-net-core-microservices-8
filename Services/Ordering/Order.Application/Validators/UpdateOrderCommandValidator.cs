using FluentValidation;
using Order.Application.Commands;

namespace Order.Application.Validators;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.id)
            .NotEmpty()
            .NotNull()
            .WithMessage("{id} is required")
            .GreaterThan(0)
            .WithMessage("{id} can not be -ve");

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
