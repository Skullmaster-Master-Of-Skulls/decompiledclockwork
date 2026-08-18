using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000C0 RID: 192
	public class PagePropertiesChangingEventArgs : EventArgs
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x00024179 File Offset: 0x00022379
		public PagePropertiesChangingEventArgs(int startRowIndex, int maximumRows)
		{
			this._startRowIndex = startRowIndex;
			this._maximumRows = maximumRows;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0002418F File Offset: 0x0002238F
		public int MaximumRows
		{
			get
			{
				return this._maximumRows;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00024197 File Offset: 0x00022397
		public int StartRowIndex
		{
			get
			{
				return this._startRowIndex;
			}
		}

		// Token: 0x0400030E RID: 782
		private int _startRowIndex;

		// Token: 0x0400030F RID: 783
		private int _maximumRows;
	}
}
