using System;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x02000085 RID: 133
	[ServiceContract(Name = "TargetServiceResponse", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
	internal interface IDiscoveryResponseContractApril2005
	{
		// Token: 0x0600060F RID: 1551
		[OperationContract(Action = "http://schemas.xmlsoap.org/ws/2005/04/discovery/ProbeMatches", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginProbeMatchOperation(ProbeMatchesMessageApril2005 response, AsyncCallback callback, object state);

		// Token: 0x06000610 RID: 1552
		void EndProbeMatchOperation(IAsyncResult result);

		// Token: 0x06000611 RID: 1553
		[OperationContract(Action = "http://schemas.xmlsoap.org/ws/2005/04/discovery/ResolveMatches", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginResolveMatchOperation(ResolveMatchesMessageApril2005 response, AsyncCallback callback, object state);

		// Token: 0x06000612 RID: 1554
		void EndResolveMatchOperation(IAsyncResult result);

		// Token: 0x06000613 RID: 1555
		[OperationContract(Action = "http://schemas.xmlsoap.org/ws/2005/04/discovery/Hello", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginHelloOperation(HelloMessageApril2005 message, AsyncCallback callback, object state);

		// Token: 0x06000614 RID: 1556
		void EndHelloOperation(IAsyncResult result);
	}
}
