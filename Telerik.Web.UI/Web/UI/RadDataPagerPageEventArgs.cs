using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001964 RID: 6500
	public class RadDataPagerPageEventArgs : EventArgs
	{
		// Token: 0x0600FB9C RID: 64412 RVA: 0x0038B540 File Offset: 0x00389740
		public RadDataPagerPageEventArgs()
		{
		}

		// Token: 0x0600FB9D RID: 64413 RVA: 0x0038B548 File Offset: 0x00389748
		public RadDataPagerPageEventArgs(PageEventArgs originalArgs)
		{
			this._startRowIndex = originalArgs.StartRowIndex;
			this._maximumRows = originalArgs.MaximumRows;
			this._totalRowCount = originalArgs.TotalRowCount;
		}

		// Token: 0x0600FB9E RID: 64414 RVA: 0x0038B574 File Offset: 0x00389774
		public RadDataPagerPageEventArgs(int startRowIndex, int maximumRows, int totalRowCount)
		{
			this._startRowIndex = startRowIndex;
			this._maximumRows = maximumRows;
			this._totalRowCount = totalRowCount;
		}

		// Token: 0x17004C07 RID: 19463
		// (get) Token: 0x0600FB9F RID: 64415 RVA: 0x0038B591 File Offset: 0x00389791
		public int MaximumRows
		{
			get
			{
				return this._maximumRows;
			}
		}

		// Token: 0x17004C08 RID: 19464
		// (get) Token: 0x0600FBA0 RID: 64416 RVA: 0x0038B599 File Offset: 0x00389799
		public int StartRowIndex
		{
			get
			{
				return this._startRowIndex;
			}
		}

		// Token: 0x17004C09 RID: 19465
		// (get) Token: 0x0600FBA1 RID: 64417 RVA: 0x0038B5A1 File Offset: 0x003897A1
		public int TotalRowCount
		{
			get
			{
				return this._totalRowCount;
			}
		}

		// Token: 0x04004793 RID: 18323
		private int _maximumRows;

		// Token: 0x04004794 RID: 18324
		private int _startRowIndex;

		// Token: 0x04004795 RID: 18325
		private int _totalRowCount;
	}
}
