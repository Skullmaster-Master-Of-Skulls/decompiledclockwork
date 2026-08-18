using System;
using System.ServiceModel;

namespace NLog.LogReceiverService
{
	// Token: 0x0200012C RID: 300
	[ServiceContract(Namespace = "http://nlog-project.org/ws/")]
	public interface ILogReceiverOneWayServer
	{
		// Token: 0x06000A72 RID: 2674
		[OperationContract(IsOneWay = true)]
		void ProcessLogMessages(NLogEvents events);
	}
}
