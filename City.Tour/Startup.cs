using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(City.Tour.Startup))]
namespace City.Tour
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
