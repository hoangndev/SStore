using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SStore.Startup))]
namespace SStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
