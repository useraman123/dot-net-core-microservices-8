using Catalog.Application.Command;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers;

public class CatalogController(IMediator _mediator,ILogger<CatalogController> _logger) : ApiController
{

    #region GetProductById
    [HttpGet]
    [Route("[action]/{Id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductResponse>> GetProductById(string Id)
    {
        var query = new GetProductById(Id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    #endregion

    #region GetProductByName
    [HttpGet]
    [Route("[action]/{productName}", Name = "GetProductByName")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductByName(string productName)
    {
        var query = new GetProductByName(productName);
        var result = await _mediator.Send(query);
        _logger.LogInformation($"Product {productName} fetched");
        return Ok(result);
    }
    #endregion

    #region GetProductByBrand
    [HttpGet]
    [Route("[action]/{productBrand}", Name = "GetProductByBrand")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductResponse>> GetProductByBrand(string productBrand)
    {
        var query = new GetProductByBrand(productBrand);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    #endregion

    #region GetAllProducts
    [HttpGet]
    [Route("GetAllProducts")]
    [ProducesResponseType(typeof(Pagination<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Pagination<ProductResponse>>> GetAllProducts([FromQuery]CatalogSpecParam specParam)
    {
        var query = new GetAllProductsQuery(specParam);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    #endregion

    #region GetAllBrands
    [HttpGet]
    [Route("GetAllBrands")]
    [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<BrandResponse>>> GetAllBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    #endregion

    #region GetAllTypes
    [HttpGet]
    [Route("GetAllTypes")]
    [ProducesResponseType(typeof(IList<TypeResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<TypeResponse>>> GetAllTypes()
    {
        var query = new GetAllTypeQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    #endregion

    #region CreateProduct
    [HttpPost]
    [Route("CreateProduct")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    #endregion

    #region UpdateProduct
    [HttpPut]
    [Route("UpdateProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    #endregion

    #region DeleteProduct
    [HttpDelete]
    [Route("{Id}",Name="DeleteProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteProduct(string Id)
    {
        var command = new DeleteProductCommand(Id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    #endregion
}
