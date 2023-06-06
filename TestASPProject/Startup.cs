using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestASPProject.Startup))]
namespace TestASPProject
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
