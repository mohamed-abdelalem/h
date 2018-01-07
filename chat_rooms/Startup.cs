using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(chat_rooms.Startup))]
namespace chat_rooms
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
