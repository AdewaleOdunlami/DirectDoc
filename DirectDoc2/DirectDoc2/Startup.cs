using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DirectDoc2.Startup))]
namespace DirectDoc2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
