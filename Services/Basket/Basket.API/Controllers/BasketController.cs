using Basket.Application.Command;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Reponses;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers;

public class BasketController(IMediator _mediator, IPublishEndpoint _publish) : ApiController
{
    #region GetBasketByUserName
    [HttpGet]
    [Route("[action]/{userName}",Name ="GetBasketByUserName")]
    [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBasketByUserName(string userName)
    {
        var query = new GetBasketByUserNameQuery(userName);
        var basket = await _mediator.Send(query);
        return Ok(basket);
    }
    #endregion

    #region UpsertBasket
    [HttpPost]
    [Route("UpsertBasket")]
    [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpsertBasket([FromBody] CreateShoppingCartCommand command)
    {
        var basket = await _mediator.Send(command);
        return Ok(basket);
    }
    #endregion

    #region DeleteBasketByUserName
    [HttpDelete]
    [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
    public async Task<IActionResult> DeleteBasketByUserName(string userName)
    {
        var command = new DeleteBasketByUserNameCommand(userName);
        return Ok( await _mediator.Send(command));
    }
    #endregion

    #region Checkout
    [Route("[action]")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
        //get the existing basket with userName 
        var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
        var basket  = await  _mediator.Send(query);
        if(basket == null)
        {
            return BadRequest();
        }
        var eventMsg = BasketMapper.Mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMsg.TotalPrice = basket.TotalPrice;
        //eventMsg.CorrelationId = 
        //publish message via rabbitMQ
        //now the message sent to message broker basket task is over now it's ordering API job to consume this message and create order
        await _publish.Publish(eventMsg);
        //Remove the Basket
        var deleteCommand = new DeleteBasketByUserNameCommand(basketCheckout.UserName);
        await _mediator.Send(deleteCommand);
        return Accepted();
    }
    #endregion
}
