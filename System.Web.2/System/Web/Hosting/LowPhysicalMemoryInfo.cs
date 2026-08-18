using System;

namespace System.Web.Hosting
{
	// Token: 0x02000787 RID: 1927
	public sealed class LowPhysicalMemoryInfo
	{
		// Token: 0x06005C53 RID: 23635 RVA: 0x0013F80C File Offset: 0x0013DA0C
		public LowPhysicalMemoryInfo(int currentPercentUsed, int percentLimit)
		{
			this._currentPercent = currentPercentUsed;
			this._limit = percentLimit;
			this._requestGC = false;
		}

		// Token: 0x17001B09 RID: 6921
		// (get) Token: 0x06005C54 RID: 23636 RVA: 0x0013F829 File Offset: 0x0013DA29
		public int CurrentPercentUsed
		{
			get
			{
				return this._currentPercent;
			}
		}

		// Token: 0x17001B0A RID: 6922
		// (get) Token: 0x06005C55 RID: 23637 RVA: 0x0013F831 File Offset: 0x0013DA31
		public int PercentLimit
		{
			get
			{
				return this._limit;
			}
		}

		// Token: 0x17001B0B RID: 6923
		// (get) Token: 0x06005C56 RID: 23638 RVA: 0x0013F839 File Offset: 0x0013DA39
		// (set) Token: 0x06005C57 RID: 23639 RVA: 0x0013F841 File Offset: 0x0013DA41
		public bool RequestGC
		{
			get
			{
				return this._requestGC;
			}
			set
			{
				this._requestGC = (this._requestGC || value);
			}
		}

		// Token: 0x0400309A RID: 12442
		private int _currentPercent;

		// Token: 0x0400309B RID: 12443
		private int _limit;

		// Token: 0x0400309C RID: 12444
		private bool _requestGC;
	}
}
