using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FluentEFExampleCF.Startup))]
namespace FluentEFExampleCF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
