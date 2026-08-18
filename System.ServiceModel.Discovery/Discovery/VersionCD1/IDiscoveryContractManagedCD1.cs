using System;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x0200006A RID: 106
	[ServiceContract(Name = "DiscoveryProxy", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
	internal interface IDiscoveryContractManagedCD1
	{
		// Token: 0x0600054D RID: 1357
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/Probe", ReplyAction = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/ProbeMatches")]
		ProbeMatchesMessageCD1 ProbeOperation(ProbeMessageCD1 request);

		// Token: 0x0600054E RID: 1358
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/Probe", ReplyAction = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/ProbeMatches", AsyncPattern = true)]
		IAsyncResult BeginProbeOperation(ProbeMessageCD1 request, AsyncCallback callback, object state);

		// Token: 0x0600054F RID: 1359
		ProbeMatchesMessageCD1 EndProbeOperation(IAsyncResult result);

		// Token: 0x06000550 RID: 1360
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/Resolve", ReplyAction = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/ResolveMatches")]
		ResolveMatchesMessageCD1 ResolveOperation(ResolveMessageCD1 request);

		// Token: 0x06000551 RID: 1361
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/Resolve", ReplyAction = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/ResolveMatches", AsyncPattern = true)]
		IAsyncResult BeginResolveOperation(ResolveMessageCD1 request, AsyncCallback callback, object state);

		// Token: 0x06000552 RID: 1362
		ResolveMatchesMessageCD1 EndResolveOperation(IAsyncResult result);
	}
}
