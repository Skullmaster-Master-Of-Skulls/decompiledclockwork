using System;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x0200009C RID: 156
	[ServiceContract(Name = "DiscoveryProxy", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
	internal interface IDiscoveryContractManaged11
	{
		// Token: 0x060006CD RID: 1741
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/Probe", ReplyAction = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/ProbeMatches")]
		ProbeMatchesMessage11 ProbeOperation(ProbeMessage11 request);

		// Token: 0x060006CE RID: 1742
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/Probe", ReplyAction = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/ProbeMatches", AsyncPattern = true)]
		IAsyncResult BeginProbeOperation(ProbeMessage11 request, AsyncCallback callback, object state);

		// Token: 0x060006CF RID: 1743
		ProbeMatchesMessage11 EndProbeOperation(IAsyncResult result);

		// Token: 0x060006D0 RID: 1744
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/Resolve", ReplyAction = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/ResolveMatches")]
		ResolveMatchesMessage11 ResolveOperation(ResolveMessage11 request);

		// Token: 0x060006D1 RID: 1745
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/Resolve", ReplyAction = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/ResolveMatches", AsyncPattern = true)]
		IAsyncResult BeginResolveOperation(ResolveMessage11 request, AsyncCallback callback, object state);

		// Token: 0x060006D2 RID: 1746
		ResolveMatchesMessage11 EndResolveOperation(IAsyncResult result);
	}
}
