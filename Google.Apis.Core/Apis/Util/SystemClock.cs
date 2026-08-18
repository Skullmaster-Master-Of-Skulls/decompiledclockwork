using System;

namespace Google.Apis.Util
{
	// Token: 0x02000008 RID: 8
	public class SystemClock : IClock
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002227 File Offset: 0x00000427
		protected SystemClock()
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000222F File Offset: 0x0000042F
		public DateTime Now
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002236 File Offset: 0x00000436
		public DateTime UtcNow
		{
			get
			{
				return DateTime.UtcNow;
			}
		}

		// Token: 0x04000009 RID: 9
		public static readonly IClock Default = new SystemClock();
	}
}
