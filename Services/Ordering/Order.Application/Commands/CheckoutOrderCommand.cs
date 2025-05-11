using MediatR;

namespace Order.Application.Commands;

public record CheckoutOrderCommand(
    string? UserName,
    decimal? TotalPrice,
    string? FirstName,
    string? LastName,
    string? EmailAddress,
    string? AddressLine,
    string? Country,
    string? State,
    string? ZipCode,
    string? CardName,
    string? CardNumber,
    string? Expiration,
    string? Cvv,
    int? PaymentMethod
):IRequest<int>;
