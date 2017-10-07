using AutoMapper;

namespace MagicCuisine.Infrastructure
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}