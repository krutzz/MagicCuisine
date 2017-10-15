using AutoMapper;

namespace MagicCuisine.Providers
{
    public class MapProvider : IMapProvider
    {
        public T GetMap<T>(object source)
        {
            return Mapper.Map<T>(source);
        }
    }
}