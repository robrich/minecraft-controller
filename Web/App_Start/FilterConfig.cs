using System.Web.Mvc;

namespace MinecraftController.Web.App_Start {
    public static class FilterConfig {

        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RequireHttpsAttribute());
        }

    }
}
