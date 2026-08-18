using System;

namespace System.ServiceModel.Administration
{
	// Token: 0x0200044D RID: 1101
	internal class ServiceAppDomainAssociationProvider : ProviderBase, IWmiProvider
	{
		// Token: 0x06002AD9 RID: 10969 RVA: 0x000A7514 File Offset: 0x000A5714
		void IWmiProvider.EnumInstances(IWmiInstances instances)
		{
			foreach (ServiceInfo serviceInfo in new ServiceInfoCollection(ManagementExtension.Services))
			{
				IWmiInstance wmiInstance = instances.NewInstance(null);
				wmiInstance.SetProperty("AppDomainInfo", AppDomainInstanceProvider.GetReference());
				wmiInstance.SetProperty("Service", ServiceInstanceProvider.GetReference(serviceInfo));
				instances.AddInstance(wmiInstance);
			}
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000A7590 File Offset: 0x000A5790
		bool IWmiProvider.GetInstance(IWmiInstance instance)
		{
			string value = instance.GetProperty("Service") as string;
			string value2 = instance.GetProperty("AppDomainInfo") as string;
			return !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2);
		}
	}
}
