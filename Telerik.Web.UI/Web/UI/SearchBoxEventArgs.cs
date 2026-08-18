using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000EE3 RID: 3811
	public class SearchBoxEventArgs : EventArgs
	{
		// Token: 0x060090B4 RID: 37044 RVA: 0x00209BA1 File Offset: 0x00207DA1
		public SearchBoxEventArgs(string text, string value, object dataItem)
		{
			this.Text = text;
			this.Value = value;
			this.DataItem = dataItem;
		}

		// Token: 0x17002DD2 RID: 11730
		// (get) Token: 0x060090B5 RID: 37045 RVA: 0x00209BBE File Offset: 0x00207DBE
		// (set) Token: 0x060090B6 RID: 37046 RVA: 0x00209BC6 File Offset: 0x00207DC6
		public string Text { get; set; }

		// Token: 0x17002DD3 RID: 11731
		// (get) Token: 0x060090B7 RID: 37047 RVA: 0x00209BCF File Offset: 0x00207DCF
		// (set) Token: 0x060090B8 RID: 37048 RVA: 0x00209BD7 File Offset: 0x00207DD7
		public string Value { get; set; }

		// Token: 0x17002DD4 RID: 11732
		// (get) Token: 0x060090B9 RID: 37049 RVA: 0x00209BE0 File Offset: 0x00207DE0
		// (set) Token: 0x060090BA RID: 37050 RVA: 0x00209BE8 File Offset: 0x00207DE8
		public object DataItem { get; set; }
	}
}
