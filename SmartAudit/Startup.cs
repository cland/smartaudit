using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartAudit.Startup))]
namespace SmartAudit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
