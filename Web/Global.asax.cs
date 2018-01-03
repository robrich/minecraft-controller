using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MinecraftController.Web.App_Start;

namespace MinecraftController.Web {
    public class MvcApplication : HttpApplication {

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}

	}
}
