using System;

namespace Telerik.Web.UI
{
	// Token: 0x020019F8 RID: 6648
	public class GridCustomAggregateEventArgs : EventArgs
	{
		// Token: 0x06010182 RID: 65922 RVA: 0x0039E311 File Offset: 0x0039C511
		public GridCustomAggregateEventArgs(GridItem item, GridColumn column, string result)
		{
			this._result = result;
			this._item = item;
			this._column = column;
		}

		// Token: 0x17004DAF RID: 19887
		// (get) Token: 0x06010183 RID: 65923 RVA: 0x0039E339 File Offset: 0x0039C539
		public GridItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x17004DB0 RID: 19888
		// (get) Token: 0x06010184 RID: 65924 RVA: 0x0039E341 File Offset: 0x0039C541
		public GridColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17004DB1 RID: 19889
		// (get) Token: 0x06010185 RID: 65925 RVA: 0x0039E349 File Offset: 0x0039C549
		// (set) Token: 0x06010186 RID: 65926 RVA: 0x0039E351 File Offset: 0x0039C551
		public object Result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		// Token: 0x040048E5 RID: 18661
		private object _result = "";

		// Token: 0x040048E6 RID: 18662
		private GridItem _item;

		// Token: 0x040048E7 RID: 18663
		private GridColumn _column;
	}
}
