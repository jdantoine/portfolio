using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AntoinePortfolio.Startup))]
namespace AntoinePortfolio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
