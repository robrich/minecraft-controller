namespace MinecraftController.Stopper {
    using System;
    using MinecraftController.Data;
    using MinecraftController.Data.Models;

    public static class Program {
		private static readonly AzureRepository azureRepository;
		private static readonly SubscriptionRepository subscriptionRepository;

		static Program() {
			azureRepository = new AzureRepository();
			subscriptionRepository = new SubscriptionRepository();
		}

		// TODO: move these to AppSettings?
		const int sequence = 1;
        const string vmName1 = "homecraft";
        const string vmName2 = "homecraft2";
        const string vmName3 = "homecraft3";

        public static void Main(string[] args) {

			Subscription subscription = subscriptionRepository.GetSubscriptionSequence(sequence);
			if (subscription == null) {
				throw new ArgumentException("Subscription sequence " + sequence + " is null");
            }
            azureRepository.SetVm(subscription, vmName1, SetToRunning: false);
            azureRepository.SetVm(subscription, vmName2, SetToRunning: false);
            azureRepository.SetVm(subscription, vmName3, SetToRunning: false);

        }
	}
}
