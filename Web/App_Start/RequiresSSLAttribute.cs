using System.Web;
using System.Web.Mvc;

namespace MinecraftController.Web.App_Start {
    public class RequiresSSLAttribute : ActionFilterAttribute {

		public override void OnActionExecuting( ActionExecutingContext Context ) {
			HttpRequestBase req = Context.HttpContext.Request;

		    if (!this.IsSecureConnection(req)) {
		        string url = "https://" + req.Url.Authority + req.Url.PathAndQuery;
		        Context.Result = new RedirectResult(url, permanent: true);
		    }

		    base.OnActionExecuting( Context );
		}

		private bool IsSecureConnection(HttpRequestBase Request) {
			if (Request.IsSecureConnection) {
				return true;
			}
            /*
			if (ConfigurationManager.AppSettings["TrustXForwardedHeader"].ToBoolOrNull() ?? false) {
				string isHttps = Request.Headers["X-Forwarded-Proto"];
				if (!string.IsNullOrEmpty(isHttps) && isHttps.ToLowerInvariant() == "https") {
					return true;
				}
			}
            */
			return false;
		}

	}
}
