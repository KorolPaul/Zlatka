using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zlatka.Startup))]
namespace Zlatka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
