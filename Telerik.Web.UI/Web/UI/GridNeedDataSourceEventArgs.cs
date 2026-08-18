using System;

namespace Telerik.Web.UI
{
	// Token: 0x020011AF RID: 4527
	public class GridNeedDataSourceEventArgs : EventArgs
	{
		// Token: 0x0600B9E0 RID: 47584 RVA: 0x002940C7 File Offset: 0x002922C7
		public GridNeedDataSourceEventArgs(GridRebindReason RebindReason)
		{
			this._rebindReason = RebindReason;
		}

		// Token: 0x0600B9E1 RID: 47585 RVA: 0x002940D6 File Offset: 0x002922D6
		public GridNeedDataSourceEventArgs(GridRebindReason RebindReason, int startRowIndex, int rowsCount)
		{
			this._rebindReason = RebindReason;
			this.StartRowIndex = startRowIndex;
			this.RowsCount = rowsCount;
		}

		// Token: 0x17003C01 RID: 15361
		// (get) Token: 0x0600B9E2 RID: 47586 RVA: 0x002940F3 File Offset: 0x002922F3
		public bool IsFromDetailTable
		{
			get
			{
				return (this._rebindReason & GridRebindReason.DetailTableBinding) == GridRebindReason.DetailTableBinding;
			}
		}

		// Token: 0x17003C02 RID: 15362
		// (get) Token: 0x0600B9E3 RID: 47587 RVA: 0x00294100 File Offset: 0x00292300
		public GridRebindReason RebindReason
		{
			get
			{
				return this._rebindReason;
			}
		}

		// Token: 0x17003C03 RID: 15363
		// (get) Token: 0x0600B9E4 RID: 47588 RVA: 0x00294108 File Offset: 0x00292308
		// (set) Token: 0x0600B9E5 RID: 47589 RVA: 0x00294110 File Offset: 0x00292310
		public int StartRowIndex { get; private set; }

		// Token: 0x17003C04 RID: 15364
		// (get) Token: 0x0600B9E6 RID: 47590 RVA: 0x00294119 File Offset: 0x00292319
		// (set) Token: 0x0600B9E7 RID: 47591 RVA: 0x00294121 File Offset: 0x00292321
		public int RowsCount { get; private set; }

		// Token: 0x04003128 RID: 12584
		private GridRebindReason _rebindReason;
	}
}
