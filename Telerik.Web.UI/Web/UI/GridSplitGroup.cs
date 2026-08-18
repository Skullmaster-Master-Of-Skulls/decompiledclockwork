using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010C9 RID: 4297
	public class GridSplitGroup
	{
		// Token: 0x1700389F RID: 14495
		// (get) Token: 0x0600AF5B RID: 44891 RVA: 0x0025F730 File Offset: 0x0025D930
		// (set) Token: 0x0600AF5C RID: 44892 RVA: 0x0025F738 File Offset: 0x0025D938
		public int GroupItemsCount
		{
			get
			{
				return this._groupItemsCount;
			}
			set
			{
				this._groupItemsCount = value;
			}
		}

		// Token: 0x170038A0 RID: 14496
		// (get) Token: 0x0600AF5D RID: 44893 RVA: 0x0025F741 File Offset: 0x0025D941
		// (set) Token: 0x0600AF5E RID: 44894 RVA: 0x0025F749 File Offset: 0x0025D949
		public int ActualItemCount
		{
			get
			{
				return this._actualItemCount;
			}
			set
			{
				this._actualItemCount = value;
			}
		}

		// Token: 0x170038A1 RID: 14497
		// (get) Token: 0x0600AF5F RID: 44895 RVA: 0x0025F752 File Offset: 0x0025D952
		// (set) Token: 0x0600AF60 RID: 44896 RVA: 0x0025F75A File Offset: 0x0025D95A
		public GridGroupSplitMode Mode
		{
			get
			{
				return this._countinued;
			}
			set
			{
				this._countinued = value;
			}
		}

		// Token: 0x04002E37 RID: 11831
		private int _groupItemsCount;

		// Token: 0x04002E38 RID: 11832
		private int _actualItemCount;

		// Token: 0x04002E39 RID: 11833
		private GridGroupSplitMode _countinued;
	}
}
