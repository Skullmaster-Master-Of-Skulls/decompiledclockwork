using System;
using System.ServiceModel;

namespace NLog.LogReceiverService
{
	// Token: 0x0200012B RID: 299
	[ServiceContract(Namespace = "http://nlog-project.org/ws/", ConfigurationName = "NLog.LogReceiverService.ILogReceiverOneWayClient")]
	public interface ILogReceiverOneWayClient
	{
		// Token: 0x06000A70 RID: 2672
		[OperationContract(IsOneWay = true, AsyncPattern = true, Action = "http://nlog-project.org/ws/ILogReceiverOneWayServer/ProcessLogMessages")]
		IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState);

		// Token: 0x06000A71 RID: 2673
		void EndProcessLogMessages(IAsyncResult result);
	}
}
