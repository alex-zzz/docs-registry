using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DocsRegistry.Startup))]
namespace DocsRegistry
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
