using System;

namespace System.IO
{
	// Token: 0x02000406 RID: 1030
	public struct WaitForChangedResult
	{
		// Token: 0x060026AF RID: 9903 RVA: 0x000B227B File Offset: 0x000B047B
		internal WaitForChangedResult(WatcherChangeTypes changeType, string name, bool timedOut)
		{
			this = new WaitForChangedResult(changeType, name, null, timedOut);
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000B2287 File Offset: 0x000B0487
		internal WaitForChangedResult(WatcherChangeTypes changeType, string name, string oldName, bool timedOut)
		{
			this.changeType = changeType;
			this.name = name;
			this.oldName = oldName;
			this.timedOut = timedOut;
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060026B1 RID: 9905 RVA: 0x000B22A6 File Offset: 0x000B04A6
		// (set) Token: 0x060026B2 RID: 9906 RVA: 0x000B22AE File Offset: 0x000B04AE
		public WatcherChangeTypes ChangeType
		{
			get
			{
				return this.changeType;
			}
			set
			{
				this.changeType = value;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x000B22B7 File Offset: 0x000B04B7
		// (set) Token: 0x060026B4 RID: 9908 RVA: 0x000B22BF File Offset: 0x000B04BF
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060026B5 RID: 9909 RVA: 0x000B22C8 File Offset: 0x000B04C8
		// (set) Token: 0x060026B6 RID: 9910 RVA: 0x000B22D0 File Offset: 0x000B04D0
		public string OldName
		{
			get
			{
				return this.oldName;
			}
			set
			{
				this.oldName = value;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060026B7 RID: 9911 RVA: 0x000B22D9 File Offset: 0x000B04D9
		// (set) Token: 0x060026B8 RID: 9912 RVA: 0x000B22E1 File Offset: 0x000B04E1
		public bool TimedOut
		{
			get
			{
				return this.timedOut;
			}
			set
			{
				this.timedOut = value;
			}
		}

		// Token: 0x040020EC RID: 8428
		private WatcherChangeTypes changeType;

		// Token: 0x040020ED RID: 8429
		private string name;

		// Token: 0x040020EE RID: 8430
		private string oldName;

		// Token: 0x040020EF RID: 8431
		private bool timedOut;

		// Token: 0x040020F0 RID: 8432
		internal static readonly WaitForChangedResult TimedOutResult = new WaitForChangedResult((WatcherChangeTypes)0, null, true);
	}
}
