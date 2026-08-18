using System;

namespace System.Windows.Forms
{
	// Token: 0x02000148 RID: 328
	public class CacheVirtualItemsEventArgs : EventArgs
	{
		// Token: 0x06000CD8 RID: 3288 RVA: 0x00024E8F File Offset: 0x0002308F
		public CacheVirtualItemsEventArgs(int startIndex, int endIndex)
		{
			this.startIndex = startIndex;
			this.endIndex = endIndex;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00024EA5 File Offset: 0x000230A5
		public int StartIndex
		{
			get
			{
				return this.startIndex;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00024EAD File Offset: 0x000230AD
		public int EndIndex
		{
			get
			{
				return this.endIndex;
			}
		}

		// Token: 0x0400075A RID: 1882
		private int startIndex;

		// Token: 0x0400075B RID: 1883
		private int endIndex;
	}
}
