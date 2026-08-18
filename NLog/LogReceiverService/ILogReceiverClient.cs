using System;
using System.ServiceModel;

namespace NLog.LogReceiverService
{
	// Token: 0x0200012A RID: 298
	[ServiceContract(Namespace = "http://nlog-project.org/ws/", ConfigurationName = "NLog.LogReceiverService.ILogReceiverClient")]
	[Obsolete("This may be removed in a future release.  Use ILogReceiverOneWayClient or ILogReceiverTwoWayClient.")]
	public interface ILogReceiverClient
	{
		// Token: 0x06000A6E RID: 2670
		[OperationContract(AsyncPattern = true, Action = "http://nlog-project.org/ws/ILogReceiverServer/ProcessLogMessages", ReplyAction = "http://nlog-project.org/ws/ILogReceiverServer/ProcessLogMessagesResponse")]
		IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState);

		// Token: 0x06000A6F RID: 2671
		void EndProcessLogMessages(IAsyncResult result);
	}
}
