﻿using AutoMapper;
using Catalog.Application.Command;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specification;

namespace Catalog.Application.Mappers;

public class ProductMappingProfile:Profile
{
    public ProductMappingProfile()
    {
        // Mapper Source -> Destination 
        CreateMap<ProductBrand, BrandResponse>().ReverseMap();
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<ProductType, TypeResponse>().ReverseMap();
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Pagination<Product>, Pagination<ProductResponse>>().ReverseMap();
    }
}
