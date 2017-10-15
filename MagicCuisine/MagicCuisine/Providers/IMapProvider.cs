namespace MagicCuisine.Providers
{
    public interface IMapProvider
    {
        T GetMap<T>(object source);
    }
}