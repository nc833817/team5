using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(team5_centric.Startup))]
namespace team5_centric
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
