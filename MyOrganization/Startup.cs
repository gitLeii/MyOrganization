using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyOrganization.Startup))]
namespace MyOrganization
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
