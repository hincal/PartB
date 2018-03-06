using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Part_B.Startup))]
namespace Part_B
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
