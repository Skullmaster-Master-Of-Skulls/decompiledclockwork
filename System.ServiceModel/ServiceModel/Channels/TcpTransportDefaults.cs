using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000791 RID: 1937
	internal static class TcpTransportDefaults
	{
		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x060049A9 RID: 18857 RVA: 0x0010EC7C File Offset: 0x0010CE7C
		internal static TimeSpan ConnectionLeaseTimeout
		{
			get
			{
				return TimeSpanHelper.FromMinutes(5, "00:05:00");
			}
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x0010EC89 File Offset: 0x0010CE89
		internal static int GetListenBacklog()
		{
			if (OSEnvironmentHelper.IsApplicationTargeting45)
			{
				return 12 * OSEnvironmentHelper.ProcessorCount;
			}
			return 10;
		}

		// Token: 0x04002E8A RID: 11914
		internal const int ListenBacklogConst = 0;

		// Token: 0x04002E8B RID: 11915
		internal const string ConnectionLeaseTimeoutString = "00:05:00";

		// Token: 0x04002E8C RID: 11916
		internal const bool PortSharingEnabled = false;

		// Token: 0x04002E8D RID: 11917
		internal const bool TeredoEnabled = false;

		// Token: 0x04002E8E RID: 11918
		private const int ListenBacklogPre45 = 10;
	}
}
