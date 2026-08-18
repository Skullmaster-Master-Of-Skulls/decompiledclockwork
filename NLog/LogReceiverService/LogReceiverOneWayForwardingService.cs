using System;

namespace NLog.LogReceiverService
{
	// Token: 0x02000131 RID: 305
	public class LogReceiverOneWayForwardingService : BaseLogReceiverForwardingService, ILogReceiverOneWayServer
	{
		// Token: 0x06000A8C RID: 2700 RVA: 0x00018EB8 File Offset: 0x000170B8
		public LogReceiverOneWayForwardingService() : this(null)
		{
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00018EC1 File Offset: 0x000170C1
		public LogReceiverOneWayForwardingService(LogFactory logFactory) : base(logFactory)
		{
		}
	}
}
