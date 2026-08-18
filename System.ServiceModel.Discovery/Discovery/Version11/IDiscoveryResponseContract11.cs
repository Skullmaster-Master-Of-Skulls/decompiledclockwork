using System;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x0200009D RID: 157
	[ServiceContract(Name = "TargetServiceResponse", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
	internal interface IDiscoveryResponseContract11
	{
		// Token: 0x060006D3 RID: 1747
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/ProbeMatches", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginProbeMatchOperation(ProbeMatchesMessage11 response, AsyncCallback callback, object state);

		// Token: 0x060006D4 RID: 1748
		void EndProbeMatchOperation(IAsyncResult result);

		// Token: 0x060006D5 RID: 1749
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/ResolveMatches", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginResolveMatchOperation(ResolveMatchesMessage11 response, AsyncCallback callback, object state);

		// Token: 0x060006D6 RID: 1750
		void EndResolveMatchOperation(IAsyncResult result);

		// Token: 0x060006D7 RID: 1751
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/Hello", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginHelloOperation(HelloMessage11 message, AsyncCallback callback, object state);

		// Token: 0x060006D8 RID: 1752
		void EndHelloOperation(IAsyncResult result);
	}
}
