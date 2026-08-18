using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000090 RID: 144
	public class DataPagerCommandEventArgs : CommandEventArgs
	{
		// Token: 0x06000652 RID: 1618 RVA: 0x0001BF13 File Offset: 0x0001A113
		public DataPagerCommandEventArgs(DataPagerField pagerField, int totalRowCount, CommandEventArgs originalArgs, DataPagerFieldItem item) : base(originalArgs)
		{
			this._pagerField = pagerField;
			this._totalRowCount = totalRowCount;
			this._item = item;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0001BF40 File Offset: 0x0001A140
		public DataPagerFieldItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001BF48 File Offset: 0x0001A148
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0001BF50 File Offset: 0x0001A150
		public int NewMaximumRows
		{
			get
			{
				return this._newMaximumRows;
			}
			set
			{
				this._newMaximumRows = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001BF59 File Offset: 0x0001A159
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0001BF61 File Offset: 0x0001A161
		public int NewStartRowIndex
		{
			get
			{
				return this._newStartRowIndex;
			}
			set
			{
				this._newStartRowIndex = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001BF6A File Offset: 0x0001A16A
		public DataPagerField PagerField
		{
			get
			{
				return this._pagerField;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001BF72 File Offset: 0x0001A172
		public int TotalRowCount
		{
			get
			{
				return this._totalRowCount;
			}
		}

		// Token: 0x04000244 RID: 580
		private DataPagerField _pagerField;

		// Token: 0x04000245 RID: 581
		private int _totalRowCount;

		// Token: 0x04000246 RID: 582
		private int _newMaximumRows = -1;

		// Token: 0x04000247 RID: 583
		private int _newStartRowIndex = -1;

		// Token: 0x04000248 RID: 584
		private DataPagerFieldItem _item;
	}
}
