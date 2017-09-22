using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MagicCuisine.Startup))]
namespace MagicCuisine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
