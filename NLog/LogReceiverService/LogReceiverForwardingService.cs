using System;

namespace NLog.LogReceiverService
{
	// Token: 0x02000130 RID: 304
	public class LogReceiverForwardingService : BaseLogReceiverForwardingService, ILogReceiverServer
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x00018EA6 File Offset: 0x000170A6
		public LogReceiverForwardingService() : this(null)
		{
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00018EAF File Offset: 0x000170AF
		public LogReceiverForwardingService(LogFactory logFactory) : base(logFactory)
		{
		}
	}
}
