using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PowerRankingOrganizer.Startup))]
namespace PowerRankingOrganizer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
