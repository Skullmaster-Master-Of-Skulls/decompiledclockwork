using System;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x02000083 RID: 131
	[ServiceContract(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery", CallbackContract = typeof(IDiscoveryResponseContractApril2005))]
	internal interface IDiscoveryContractApril2005
	{
		// Token: 0x06000609 RID: 1545
		[OperationContract(Action = "http://schemas.xmlsoap.org/ws/2005/04/discovery/Probe", IsOneWay = true)]
		void ProbeOperation(ProbeMessageApril2005 request);

		// Token: 0x0600060A RID: 1546
		[OperationContract(Action = "http://schemas.xmlsoap.org/ws/2005/04/discovery/Probe", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginProbeOperation(ProbeMessageApril2005 request, AsyncCallback callback, object state);

		// Token: 0x0600060B RID: 1547
		void EndProbeOperation(IAsyncResult result);

		// Token: 0x0600060C RID: 1548
		[OperationContract(Action = "http://schemas.xmlsoap.org/ws/2005/04/discovery/Resolve", IsOneWay = true)]
		void ResolveOperation(ResolveMessageApril2005 request);

		// Token: 0x0600060D RID: 1549
		[OperationContract(Action = "http://schemas.xmlsoap.org/ws/2005/04/discovery/Resolve", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginResolveOperation(ResolveMessageApril2005 request, AsyncCallback callback, object state);

		// Token: 0x0600060E RID: 1550
		void EndResolveOperation(IAsyncResult result);
	}
}
