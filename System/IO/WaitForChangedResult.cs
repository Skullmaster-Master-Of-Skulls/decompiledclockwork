using System;

namespace System.IO
{
	// Token: 0x02000733 RID: 1843
	public struct WaitForChangedResult
	{
		// Token: 0x06003849 RID: 14409 RVA: 0x000EDA71 File Offset: 0x000ECA71
		internal WaitForChangedResult(WatcherChangeTypes changeType, string name, bool timedOut)
		{
			this = new WaitForChangedResult(changeType, name, null, timedOut);
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x000EDA7D File Offset: 0x000ECA7D
		internal WaitForChangedResult(WatcherChangeTypes changeType, string name, string oldName, bool timedOut)
		{
			this.changeType = changeType;
			this.name = name;
			this.oldName = oldName;
			this.timedOut = timedOut;
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x000EDA9C File Offset: 0x000ECA9C
		// (set) Token: 0x0600384C RID: 14412 RVA: 0x000EDAA4 File Offset: 0x000ECAA4
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

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000EDAAD File Offset: 0x000ECAAD
		// (set) Token: 0x0600384E RID: 14414 RVA: 0x000EDAB5 File Offset: 0x000ECAB5
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

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x000EDABE File Offset: 0x000ECABE
		// (set) Token: 0x06003850 RID: 14416 RVA: 0x000EDAC6 File Offset: 0x000ECAC6
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

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06003851 RID: 14417 RVA: 0x000EDACF File Offset: 0x000ECACF
		// (set) Token: 0x06003852 RID: 14418 RVA: 0x000EDAD7 File Offset: 0x000ECAD7
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

		// Token: 0x04003237 RID: 12855
		private WatcherChangeTypes changeType;

		// Token: 0x04003238 RID: 12856
		private string name;

		// Token: 0x04003239 RID: 12857
		private string oldName;

		// Token: 0x0400323A RID: 12858
		private bool timedOut;

		// Token: 0x0400323B RID: 12859
		internal static readonly WaitForChangedResult TimedOutResult = new WaitForChangedResult((WatcherChangeTypes)0, null, true);
	}
}
