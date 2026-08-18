using System;
using System.ServiceModel;

namespace NLog.LogReceiverService
{
	// Token: 0x0200012D RID: 301
	[ServiceContract(Namespace = "http://nlog-project.org/ws/")]
	public interface ILogReceiverServer
	{
		// Token: 0x06000A73 RID: 2675
		[OperationContract]
		void ProcessLogMessages(NLogEvents events);
	}
}
