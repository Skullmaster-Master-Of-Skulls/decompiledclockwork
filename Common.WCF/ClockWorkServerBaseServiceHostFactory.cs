using System;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using TechnoPro.Common.WCF.Adapters;
using TechnoPro.Common.WCF.Attributes;

namespace TechnoPro.Common.WCF
{
	// Token: 0x0200000C RID: 12
	public class ClockWorkServerBaseServiceHostFactory : ServiceHostFactory
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00003138 File Offset: 0x00001338
		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			Type contractType = serviceType.Name.GetContractType();
			bool flag = contractType.GetCustomAttribute<DiscoverServiceAttribute>() != null;
			ServiceHost result;
			if (flag)
			{
				result = new ClockWorkServerDiscoveryServiceHost(serviceType, baseAddresses);
			}
			else
			{
				result = new ClockWorkServerBaseServiceHost(serviceType, baseAddresses);
			}
			return result;
		}
	}
}
