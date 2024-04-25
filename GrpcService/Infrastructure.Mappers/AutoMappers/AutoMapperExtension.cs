using Application.Mapper;
using Infrastructure.Mappers.AutoMappers.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Mappers.AutoMappers;

public static class AutoMapperExtension
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddSingleton<IMapper, AutoMapperDI>();
        services.AddSingleton<AutoMapper.IMapper>(new AutoMapper.Mapper(MapperBuilder()));
        return services;
    }

    private static AutoMapper.MapperConfiguration MapperBuilder()
    {
        return new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddStudent();               
        });
    }
}
