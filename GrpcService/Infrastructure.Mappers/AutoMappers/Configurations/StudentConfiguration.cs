using AutoMapper;
using EntityStudent = Domain.Entities.Student;
using DtoStudent = Api.Students.Student;
namespace Infrastructure.Mappers.AutoMappers.Configurations
{
    public static class StudentConfiguration
    {
        public static IMapperConfigurationExpression AddStudent(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<EntityStudent, DtoStudent>().ReverseMap();
            return cfg;
        }
    }
}
