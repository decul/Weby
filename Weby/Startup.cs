using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Weby.Startup))]
namespace Weby
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
