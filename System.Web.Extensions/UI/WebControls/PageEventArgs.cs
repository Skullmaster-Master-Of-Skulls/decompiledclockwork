using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000BF RID: 191
	public class PageEventArgs : EventArgs
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x00024144 File Offset: 0x00022344
		public PageEventArgs(int startRowIndex, int maximumRows, int totalRowCount)
		{
			this._startRowIndex = startRowIndex;
			this._maximumRows = maximumRows;
			this._totalRowCount = totalRowCount;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00024161 File Offset: 0x00022361
		public int MaximumRows
		{
			get
			{
				return this._maximumRows;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00024169 File Offset: 0x00022369
		public int StartRowIndex
		{
			get
			{
				return this._startRowIndex;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00024171 File Offset: 0x00022371
		public int TotalRowCount
		{
			get
			{
				return this._totalRowCount;
			}
		}

		// Token: 0x0400030B RID: 779
		private int _startRowIndex;

		// Token: 0x0400030C RID: 780
		private int _maximumRows;

		// Token: 0x0400030D RID: 781
		private int _totalRowCount;
	}
}
