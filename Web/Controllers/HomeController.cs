using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace MinecraftController.Web.Controllers {
    public class HomeController : Controller {

		public ActionResult Index() {
			return this.View();
		}

		[HttpPost]
		public ActionResult Index(string Username, string Password) {
			// This auth is quite lame, but this site doesn't need more
			if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && Username == ConfigurationManager.AppSettings["LoginUsername"] && Password == ConfigurationManager.AppSettings["LoginPassword"]) {
				FormsAuthentication.SetAuthCookie(Username, createPersistentCookie: false);
				return this.RedirectToAction("Index", "Servers");
			} else {
				return this.RedirectToAction("Index", "Home");
			}
		}

		public ActionResult Logout() {
			FormsAuthentication.SignOut();
			return this.RedirectToAction("Index", "Home");
		}

	}
}
