using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GigHub.MVC.Startup))]
namespace GigHub.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
