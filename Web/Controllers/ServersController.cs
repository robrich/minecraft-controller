using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MinecraftController.Data;
using MinecraftController.Data.Models;

namespace MinecraftController.Web.Controllers {

    [Authorize]
	public class ServersController : Controller {
		private readonly AzureRepository azureRepository;
		private readonly SubscriptionRepository subscriptionRepository;
        private readonly EnrichService enrichService;

		public ServersController() {
			this.azureRepository = new AzureRepository();
			this.subscriptionRepository = new SubscriptionRepository();
            this.enrichService = new EnrichService();
        }

		public ActionResult Index() {

			List<AzureInfo> model = new List<AzureInfo>();
			List<Subscription> subscriptions = this.subscriptionRepository.GetSubscriptions();
			foreach (Subscription subscription in subscriptions) {
				List<AzureInfo> data = this.azureRepository.GetServers(subscription);
				if (data.Count > 0) {
					model.AddRange(data);
				}
			}
		    model = this.enrichService.AddData(model);

			return this.View(model);
		}
        
		public ActionResult SetVm(string SubscriptionId, string VmName, bool SetToRunning) {

			try {
				Subscription subscription = this.subscriptionRepository.GetSubscription(SubscriptionId);
				string newStatus = this.azureRepository.SetVm(subscription, VmName, SetToRunning);

				return this.Json(new {Success = true, Status = newStatus});
			} catch (Exception ex) {
				return this.Json(new {Success = false, Message = ex.Message});
			}
		}

	}
}
