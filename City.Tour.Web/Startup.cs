using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(City.Tour.Web.Startup))]
namespace City.Tour.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
