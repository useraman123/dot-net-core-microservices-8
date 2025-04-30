using Catalog.Application.Responses;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Command;

public record CreateProductCommand(string Name, string Summary, string Description, string ImageFile, ProductBrand Brands, ProductType Types, decimal Price):IRequest<ProductResponse>;