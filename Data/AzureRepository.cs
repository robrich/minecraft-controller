using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.Compute;
using Microsoft.WindowsAzure.Management.Compute.Models;
using MinecraftController.Data.Models;

namespace MinecraftController.Data {
    public class AzureRepository {

		private CertificateCloudCredentials GetCredentials(Subscription Subscription) {
			try {
				return new CertificateCloudCredentials(
					Subscription.SubscriptionId,
					new X509Certificate2(Convert.FromBase64String(Subscription.ManagementCert), Subscription.ManagementCertPassword, X509KeyStorageFlags.MachineKeySet)
				);
				// Azure throws if you don't use MachineKeySet, http://stackoverflow.com/a/27146917/702931
				// FRAGILE: Azure still throws if you're using the cert in the publishsettings file, use a self-signed cert instead, see README.md
			} catch (Exception ex) {
				throw new Exception("Error getting certificate for " + Subscription.SubscriptionId + ": " + ex.Message);
			}
		}

		public List<AzureInfo> GetServers(Subscription Subscription) {
			List<AzureInfo> results = new List<AzureInfo>();

			CertificateCloudCredentials creds = this.GetCredentials(Subscription);
            
			using (ComputeManagementClient vmClient = new ComputeManagementClient(creds)) {
				List<HostedServiceListResponse.HostedService> vms = vmClient.HostedServices.List().ToList();
				results.AddRange(
					from vm in vms
					let vmDetails = vmClient.Deployments.GetByName(vm.ServiceName, vm.ServiceName)
                    let vmController = vmClient.VirtualMachines.Get(vm.ServiceName, vm.ServiceName, vm.ServiceName)
					select new AzureInfo {
						SubscriptionId = Subscription.SubscriptionId,
						Name = vm.ServiceName,
						Status = vmDetails.Status.ToString(),
						Location = vm.Properties.Location,
                        Size = vmController.RoleSize,
						Online = vmDetails.Status == DeploymentStatus.Running
					}
				);
			}
            
			return results;
		}
        
		public string SetVm(Subscription Subscription, string VmName, bool SetToRunning) {

			CertificateCloudCredentials creds = this.GetCredentials(Subscription);
			using (ComputeManagementClient vmClient = new ComputeManagementClient(creds)) {

				// No idea why it wants the same name again and again or when this name would not be the same

				DeploymentGetResponse response = vmClient.Deployments.GetByName(VmName, VmName);

				if (response.Status != DeploymentStatus.Running && SetToRunning) {
					// Start it
					vmClient.VirtualMachines.Start(VmName, VmName, VmName);
				}

				if (response.Status == DeploymentStatus.Running && !SetToRunning) {
					// Stop it
					vmClient.VirtualMachines.Shutdown(
						VmName, VmName, VmName,
						new VirtualMachineShutdownParameters {PostShutdownAction = PostShutdownAction.StoppedDeallocated}
					);
				}

				// What is it now?
				response = vmClient.Deployments.GetByName(VmName, VmName);
				return response.Status.ToString();
			}
		}

	}
}