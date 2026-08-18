using System;

namespace System.Diagnostics
{
	// Token: 0x020004F5 RID: 1269
	internal class ProcessThreadTimes
	{
		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600302B RID: 12331 RVA: 0x000D9C8A File Offset: 0x000D7E8A
		public DateTime StartTime
		{
			get
			{
				return DateTime.FromFileTime(this.create);
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x0600302C RID: 12332 RVA: 0x000D9C97 File Offset: 0x000D7E97
		public DateTime ExitTime
		{
			get
			{
				return DateTime.FromFileTime(this.exit);
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x000D9CA4 File Offset: 0x000D7EA4
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				return new TimeSpan(this.kernel);
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x000D9CB1 File Offset: 0x000D7EB1
		public TimeSpan UserProcessorTime
		{
			get
			{
				return new TimeSpan(this.user);
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x0600302F RID: 12335 RVA: 0x000D9CBE File Offset: 0x000D7EBE
		public TimeSpan TotalProcessorTime
		{
			get
			{
				return new TimeSpan(this.user + this.kernel);
			}
		}

		// Token: 0x04002886 RID: 10374
		internal long create;

		// Token: 0x04002887 RID: 10375
		internal long exit;

		// Token: 0x04002888 RID: 10376
		internal long kernel;

		// Token: 0x04002889 RID: 10377
		internal long user;
	}
}
