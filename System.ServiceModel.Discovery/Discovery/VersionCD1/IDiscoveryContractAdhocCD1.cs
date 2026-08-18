using System;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000069 RID: 105
	[ServiceContract(Name = "TargetService", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09", CallbackContract = typeof(IDiscoveryResponseContractCD1))]
	internal interface IDiscoveryContractAdhocCD1
	{
		// Token: 0x06000547 RID: 1351
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/Probe", IsOneWay = true)]
		void ProbeOperation(ProbeMessageCD1 request);

		// Token: 0x06000548 RID: 1352
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/Probe", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginProbeOperation(ProbeMessageCD1 request, AsyncCallback callback, object state);

		// Token: 0x06000549 RID: 1353
		void EndProbeOperation(IAsyncResult result);

		// Token: 0x0600054A RID: 1354
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/Resolve", IsOneWay = true)]
		void ResolveOperation(ResolveMessageCD1 request);

		// Token: 0x0600054B RID: 1355
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/Resolve", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginResolveOperation(ResolveMessageCD1 request, AsyncCallback callback, object state);

		// Token: 0x0600054C RID: 1356
		void EndResolveOperation(IAsyncResult result);
	}
}
