using System;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000068 RID: 104
	[ServiceContract(Name = "Client", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
	internal interface IAnnouncementContractCD1
	{
		// Token: 0x06000541 RID: 1345
		[OperationContract(IsOneWay = true, Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/Hello")]
		void HelloOperation(HelloMessageCD1 message);

		// Token: 0x06000542 RID: 1346
		[OperationContract(IsOneWay = true, Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/Hello", AsyncPattern = true)]
		IAsyncResult BeginHelloOperation(HelloMessageCD1 message, AsyncCallback callback, object state);

		// Token: 0x06000543 RID: 1347
		void EndHelloOperation(IAsyncResult result);

		// Token: 0x06000544 RID: 1348
		[OperationContract(IsOneWay = true, Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/Bye")]
		void ByeOperation(ByeMessageCD1 message);

		// Token: 0x06000545 RID: 1349
		[OperationContract(IsOneWay = true, Action = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/Bye", AsyncPattern = true)]
		IAsyncResult BeginByeOperation(ByeMessageCD1 message, AsyncCallback callback, object state);

		// Token: 0x06000546 RID: 1350
		void EndByeOperation(IAsyncResult result);
	}
}
