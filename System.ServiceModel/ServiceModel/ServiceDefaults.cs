using System;

namespace System.ServiceModel
{
	// Token: 0x02000113 RID: 275
	internal static class ServiceDefaults
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001D331 File Offset: 0x0001B531
		internal static TimeSpan ServiceHostCloseTimeout
		{
			get
			{
				return TimeSpanHelper.FromSeconds(10, "00:00:10");
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001D33F File Offset: 0x0001B53F
		internal static TimeSpan CloseTimeout
		{
			get
			{
				return TimeSpanHelper.FromMinutes(1, "00:01:00");
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001D34C File Offset: 0x0001B54C
		internal static TimeSpan OpenTimeout
		{
			get
			{
				return TimeSpanHelper.FromMinutes(1, "00:01:00");
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001D359 File Offset: 0x0001B559
		internal static TimeSpan ReceiveTimeout
		{
			get
			{
				return TimeSpanHelper.FromMinutes(10, "00:10:00");
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001D367 File Offset: 0x0001B567
		internal static TimeSpan SendTimeout
		{
			get
			{
				return TimeSpanHelper.FromMinutes(1, "00:01:00");
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001D374 File Offset: 0x0001B574
		internal static TimeSpan TransactionTimeout
		{
			get
			{
				return TimeSpanHelper.FromMinutes(1, "00:00:00");
			}
		}

		// Token: 0x04000A9A RID: 2714
		internal const string ServiceHostCloseTimeoutString = "00:00:10";

		// Token: 0x04000A9B RID: 2715
		internal const string CloseTimeoutString = "00:01:00";

		// Token: 0x04000A9C RID: 2716
		internal const string OpenTimeoutString = "00:01:00";

		// Token: 0x04000A9D RID: 2717
		internal const string ReceiveTimeoutString = "00:10:00";

		// Token: 0x04000A9E RID: 2718
		internal const string SendTimeoutString = "00:01:00";

		// Token: 0x04000A9F RID: 2719
		internal const string TransactionTimeoutString = "00:00:00";
	}
}
