using AutoMapper;

namespace Basket.Application.Mappers;

public static class BasketMapper
{
    //Lazy Mapping
    private static readonly Lazy<IMapper> MapperInstance = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<BasketMappingProfile>();
        });
        return config.CreateMapper();
    });
    // only initialised when needed
    public static IMapper Mapper => MapperInstance.Value;

}
