using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GigHub2.Startup))]
namespace GigHub2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
