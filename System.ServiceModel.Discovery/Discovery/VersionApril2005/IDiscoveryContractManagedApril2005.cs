using System;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x02000084 RID: 132
	[ServiceContract(Name = "DiscoveryProxy", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery", CallbackContract = typeof(IDiscoveryResponseContractApril2005))]
	internal interface IDiscoveryContractManagedApril2005 : IDiscoveryContractApril2005
	{
	}
}
