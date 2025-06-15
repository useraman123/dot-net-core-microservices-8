using Discount.Application.Command;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;


namespace Discount.API.Services;

public class DiscountService(IMediator _mediator,ILogger<DiscountService> _logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var query = new GetDiscountQuery(request.ProductName);
        var response = await _mediator.Send(query);
        _logger.LogInformation($"Discount for {request.ProductName} fetched");
        return new CouponModel { Id=response.Id,ProductName=response.ProductName,Description=response.Description,Amount=response.Amount};
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var command = new CreateDiscountCommand
        (
            request.Coupon.ProductName,
            request.Coupon.Description,
            request.Coupon.Amount
        );
        var response = await _mediator.Send(command);
        _logger.LogInformation($"Discount created  for {command.ProductName}");
        return new CouponModel {  ProductName = response.ProductName, Description = response.Description, Amount = response.Amount };
    }


    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var command = new UpdateDiscountCommand(request.Coupon.Id, request.Coupon.ProductName, request.Coupon.Description, request.Coupon.Amount);
        var response = await  _mediator.Send(command);
        _logger.LogInformation($"Discount updated  for {command.productName}");
        return new CouponModel {ProductName = response.ProductName, Description = response.Description, Amount = response.Amount };
    }


    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var command = new DeleteDiscountCommand(request.ProductName);
        var reponse = await _mediator.Send(command);
        _logger.LogInformation($"Discount deleted  for {command.productName}");
        return new DeleteDiscountResponse { Success = reponse };
    }

}
