using System;
using System.ServiceModel;

namespace NLog.LogReceiverService
{
	// Token: 0x0200012E RID: 302
	[ServiceContract(Namespace = "http://nlog-project.org/ws/", ConfigurationName = "NLog.LogReceiverService.ILogReceiverClient")]
	public interface ILogReceiverTwoWayClient
	{
		// Token: 0x06000A74 RID: 2676
		[OperationContract(AsyncPattern = true, Action = "http://nlog-project.org/ws/ILogReceiverServer/ProcessLogMessages", ReplyAction = "http://nlog-project.org/ws/ILogReceiverServer/ProcessLogMessagesResponse")]
		IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState);

		// Token: 0x06000A75 RID: 2677
		void EndProcessLogMessages(IAsyncResult result);
	}
}
