using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JJServicios.Web.Startup))]
namespace JJServicios.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
