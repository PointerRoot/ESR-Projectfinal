using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ESR_Project.Startup))]
namespace ESR_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
