using System;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x0200009A RID: 154
	[ServiceContract(Name = "Client", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
	internal interface IAnnouncementContract11
	{
		// Token: 0x060006C1 RID: 1729
		[OperationContract(IsOneWay = true, Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/Hello")]
		void HelloOperation(HelloMessage11 message);

		// Token: 0x060006C2 RID: 1730
		[OperationContract(IsOneWay = true, Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/Hello", AsyncPattern = true)]
		IAsyncResult BeginHelloOperation(HelloMessage11 message, AsyncCallback callback, object state);

		// Token: 0x060006C3 RID: 1731
		void EndHelloOperation(IAsyncResult result);

		// Token: 0x060006C4 RID: 1732
		[OperationContract(IsOneWay = true, Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/Bye")]
		void ByeOperation(ByeMessage11 message);

		// Token: 0x060006C5 RID: 1733
		[OperationContract(IsOneWay = true, Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/Bye", AsyncPattern = true)]
		IAsyncResult BeginByeOperation(ByeMessage11 message, AsyncCallback callback, object state);

		// Token: 0x060006C6 RID: 1734
		void EndByeOperation(IAsyncResult result);
	}
}
