using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000AD RID: 173
	public class ListViewDataItem : ListViewItem
	{
		// Token: 0x060008AE RID: 2222 RVA: 0x00022214 File Offset: 0x00020414
		public ListViewDataItem(int dataItemIndex, int displayIndex) : base(ListViewItemType.DataItem)
		{
			this._dataItemIndex = dataItemIndex;
			this._displayIndex = displayIndex;
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0002222B File Offset: 0x0002042B
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00022233 File Offset: 0x00020433
		public override object DataItem
		{
			get
			{
				return this._dataItem;
			}
			set
			{
				this._dataItem = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0002223C File Offset: 0x0002043C
		public override int DataItemIndex
		{
			get
			{
				return this._dataItemIndex;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00022244 File Offset: 0x00020444
		public override int DisplayIndex
		{
			get
			{
				return this._displayIndex;
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0002224C File Offset: 0x0002044C
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				ListViewCommandEventArgs args = new ListViewCommandEventArgs(this, source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}

		// Token: 0x040002D8 RID: 728
		private int _dataItemIndex;

		// Token: 0x040002D9 RID: 729
		private int _displayIndex;

		// Token: 0x040002DA RID: 730
		private object _dataItem;
	}
}
