using System;

namespace System.Diagnostics
{
	// Token: 0x0200077D RID: 1917
	internal class ProcessThreadTimes
	{
		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x000FC303 File Offset: 0x000FB303
		public DateTime StartTime
		{
			get
			{
				return DateTime.FromFileTime(this.create);
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06003B47 RID: 15175 RVA: 0x000FC310 File Offset: 0x000FB310
		public DateTime ExitTime
		{
			get
			{
				return DateTime.FromFileTime(this.exit);
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000FC31D File Offset: 0x000FB31D
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				return new TimeSpan(this.kernel);
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x000FC32A File Offset: 0x000FB32A
		public TimeSpan UserProcessorTime
		{
			get
			{
				return new TimeSpan(this.user);
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06003B4A RID: 15178 RVA: 0x000FC337 File Offset: 0x000FB337
		public TimeSpan TotalProcessorTime
		{
			get
			{
				return new TimeSpan(this.user + this.kernel);
			}
		}

		// Token: 0x040033E5 RID: 13285
		internal long create;

		// Token: 0x040033E6 RID: 13286
		internal long exit;

		// Token: 0x040033E7 RID: 13287
		internal long kernel;

		// Token: 0x040033E8 RID: 13288
		internal long user;
	}
}
