using Basket.Application.Command;
using Basket.Application.Queries;
using Basket.Application.Reponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers;

public class BasketController :ApiController
{
    private readonly IMediator _mediator;
    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
    }

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
}
