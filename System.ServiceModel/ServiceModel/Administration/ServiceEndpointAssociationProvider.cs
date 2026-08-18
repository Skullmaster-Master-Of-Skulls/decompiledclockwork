using System;

namespace System.ServiceModel.Administration
{
	// Token: 0x0200044E RID: 1102
	internal class ServiceEndpointAssociationProvider : ProviderBase, IWmiProvider
	{
		// Token: 0x06002ADC RID: 10972 RVA: 0x000A75DC File Offset: 0x000A57DC
		void IWmiProvider.EnumInstances(IWmiInstances instances)
		{
			foreach (ServiceInfo serviceInfo in new ServiceInfoCollection(ManagementExtension.Services))
			{
				string reference = ServiceInstanceProvider.GetReference(serviceInfo);
				foreach (EndpointInfo endpointInfo in serviceInfo.Endpoints)
				{
					IWmiInstance wmiInstance = instances.NewInstance(null);
					string value = EndpointInstanceProvider.EndpointReference(endpointInfo.ListenUri, endpointInfo.Contract.Name);
					wmiInstance.SetProperty("Endpoint", value);
					wmiInstance.SetProperty("Service", reference);
					instances.AddInstance(wmiInstance);
				}
			}
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000A76AC File Offset: 0x000A58AC
		bool IWmiProvider.GetInstance(IWmiInstance instance)
		{
			string value = instance.GetProperty("Service") as string;
			string value2 = instance.GetProperty("Endpoint") as string;
			return !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2);
		}
	}
}
