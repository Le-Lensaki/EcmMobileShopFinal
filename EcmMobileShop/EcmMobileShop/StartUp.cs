using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EcmMobileShop.App_Start.StartUp))]
namespace EcmMobileShop.App_Start
{
    public partial class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}