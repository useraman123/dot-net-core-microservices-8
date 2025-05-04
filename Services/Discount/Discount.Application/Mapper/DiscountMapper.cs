using AutoMapper;

namespace Discount.Application.Mapper;

public static class DiscountMapper
{
    //Lazy Mapping
    private static readonly Lazy<IMapper> MapperInstance = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<DiscountMappingProfile>();
        });
        return config.CreateMapper();
    });
    // only initialised when needed
    public static IMapper Mapper => MapperInstance.Value;
}
