using System;
using System.ServiceProcess;
using Common.WinServices;

namespace TechnoPro.Common.WinServices
{
	// Token: 0x02000003 RID: 3
	public static class MessaggingQueueAdapter
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002660 File Offset: 0x00000860
		public static MsmqServiceStatus GetMessagingQueueServiceStatus()
		{
			ServiceController serviceByName = WinService.GetServiceByName("MSMQ");
			if (serviceByName == null)
			{
				return MsmqServiceStatus.NotInstalled;
			}
			if (serviceByName.Status != ServiceControllerStatus.Running)
			{
				return MsmqServiceStatus.NotRunning;
			}
			return MsmqServiceStatus.Running;
		}
	}
}
