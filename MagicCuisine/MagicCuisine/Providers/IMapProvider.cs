using System.Collections.Generic;

namespace MagicCuisine.Providers
{
    public interface IMapProvider
    {
        T GetMap<T>(object source);

        ICollection<T> GetMapCollection<T>(object source);
    }
}