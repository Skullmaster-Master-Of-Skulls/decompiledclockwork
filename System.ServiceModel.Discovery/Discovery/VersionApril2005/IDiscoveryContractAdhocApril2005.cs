using System;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x02000082 RID: 130
	[ServiceContract(Name = "TargetService", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery", CallbackContract = typeof(IDiscoveryResponseContractApril2005))]
	internal interface IDiscoveryContractAdhocApril2005 : IDiscoveryContractApril2005
	{
	}
}
