using System;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x0200006B RID: 107
	[ServiceContract(Name = "TargetServiceResponse", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
	internal interface IDiscoveryResponseContractCD1
	{
		// Token: 0x06000553 RID: 1363
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/ProbeMatches", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginProbeMatchOperation(ProbeMatchesMessageCD1 response, AsyncCallback callback, object state);

		// Token: 0x06000554 RID: 1364
		void EndProbeMatchOperation(IAsyncResult result);

		// Token: 0x06000555 RID: 1365
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/ResolveMatches", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginResolveMatchOperation(ResolveMatchesMessageCD1 response, AsyncCallback callback, object state);

		// Token: 0x06000556 RID: 1366
		void EndResolveMatchOperation(IAsyncResult result);

		// Token: 0x06000557 RID: 1367
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/Hello", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginHelloOperation(HelloMessageCD1 message, AsyncCallback callback, object state);

		// Token: 0x06000558 RID: 1368
		void EndHelloOperation(IAsyncResult result);
	}
}
