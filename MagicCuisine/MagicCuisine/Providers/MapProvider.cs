using AutoMapper;
using System.Collections.Generic;

namespace MagicCuisine.Providers
{
    public class MapProvider : IMapProvider
    {
        public T GetMap<T>(object source)
        {
            return Mapper.Map<T>(source);
        }

        public ICollection<T> GetMapCollection<T>(object source)
        {
            return Mapper.Map<ICollection<T>>(source);
        }
    }
}