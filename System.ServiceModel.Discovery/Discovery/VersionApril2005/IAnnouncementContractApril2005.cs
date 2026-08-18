using System;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x02000081 RID: 129
	[ServiceContract(Name = "Client", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
	internal interface IAnnouncementContractApril2005
	{
		// Token: 0x06000603 RID: 1539
		[OperationContract(IsOneWay = true, Action = "http://schemas.xmlsoap.org/ws/2005/04/discovery/Hello")]
		void HelloOperation(HelloMessageApril2005 message);

		// Token: 0x06000604 RID: 1540
		[OperationContract(IsOneWay = true, Action = "http://schemas.xmlsoap.org/ws/2005/04/discovery/Hello", AsyncPattern = true)]
		IAsyncResult BeginHelloOperation(HelloMessageApril2005 message, AsyncCallback callback, object state);

		// Token: 0x06000605 RID: 1541
		void EndHelloOperation(IAsyncResult result);

		// Token: 0x06000606 RID: 1542
		[OperationContract(IsOneWay = true, Action = "http://schemas.xmlsoap.org/ws/2005/04/discovery/Bye")]
		void ByeOperation(ByeMessageApril2005 message);

		// Token: 0x06000607 RID: 1543
		[OperationContract(IsOneWay = true, Action = "http://schemas.xmlsoap.org/ws/2005/04/discovery/Bye", AsyncPattern = true)]
		IAsyncResult BeginByeOperation(ByeMessageApril2005 message, AsyncCallback callback, object state);

		// Token: 0x06000608 RID: 1544
		void EndByeOperation(IAsyncResult result);
	}
}
