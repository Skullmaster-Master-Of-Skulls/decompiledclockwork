using System;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x0200009B RID: 155
	[ServiceContract(Name = "TargetService", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01", CallbackContract = typeof(IDiscoveryResponseContract11))]
	internal interface IDiscoveryContractAdhoc11
	{
		// Token: 0x060006C7 RID: 1735
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/Probe", IsOneWay = true)]
		void ProbeOperation(ProbeMessage11 request);

		// Token: 0x060006C8 RID: 1736
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/Probe", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginProbeOperation(ProbeMessage11 request, AsyncCallback callback, object state);

		// Token: 0x060006C9 RID: 1737
		void EndProbeOperation(IAsyncResult result);

		// Token: 0x060006CA RID: 1738
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/Resolve", IsOneWay = true)]
		void ResolveOperation(ResolveMessage11 request);

		// Token: 0x060006CB RID: 1739
		[OperationContract(Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/Resolve", IsOneWay = true, AsyncPattern = true)]
		IAsyncResult BeginResolveOperation(ResolveMessage11 request, AsyncCallback callback, object state);

		// Token: 0x060006CC RID: 1740
		void EndResolveOperation(IAsyncResult result);
	}
}
