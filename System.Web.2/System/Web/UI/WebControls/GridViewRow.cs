using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000422 RID: 1058
	public class GridViewRow : TableRow, IDataItemContainer, INamingContainer
	{
		// Token: 0x06003398 RID: 13208 RVA: 0x000A905D File Offset: 0x000A725D
		public GridViewRow(int rowIndex, int dataItemIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			this._rowIndex = rowIndex;
			this._dataItemIndex = dataItemIndex;
			this._rowType = rowType;
			this._rowState = rowState;
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06003399 RID: 13209 RVA: 0x000A9082 File Offset: 0x000A7282
		// (set) Token: 0x0600339A RID: 13210 RVA: 0x000A908A File Offset: 0x000A728A
		public virtual object DataItem
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

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x000A9093 File Offset: 0x000A7293
		public virtual int DataItemIndex
		{
			get
			{
				return this._dataItemIndex;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x000A909B File Offset: 0x000A729B
		public virtual int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x000A90A3 File Offset: 0x000A72A3
		// (set) Token: 0x0600339E RID: 13214 RVA: 0x000A90AB File Offset: 0x000A72AB
		public virtual DataControlRowState RowState
		{
			get
			{
				return this._rowState;
			}
			set
			{
				this._rowState = value;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x0600339F RID: 13215 RVA: 0x000A90B4 File Offset: 0x000A72B4
		// (set) Token: 0x060033A0 RID: 13216 RVA: 0x000A90BC File Offset: 0x000A72BC
		public virtual DataControlRowType RowType
		{
			get
			{
				return this._rowType;
			}
			set
			{
				this._rowType = value;
			}
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000A90C8 File Offset: 0x000A72C8
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				GridViewCommandEventArgs args = new GridViewCommandEventArgs(this, source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x060033A2 RID: 13218 RVA: 0x000A90F6 File Offset: 0x000A72F6
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.DataItem;
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x060033A3 RID: 13219 RVA: 0x000A90FE File Offset: 0x000A72FE
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.DataItemIndex;
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x060033A4 RID: 13220 RVA: 0x000A9106 File Offset: 0x000A7306
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.RowIndex;
			}
		}

		// Token: 0x0400216E RID: 8558
		private int _rowIndex;

		// Token: 0x0400216F RID: 8559
		private int _dataItemIndex;

		// Token: 0x04002170 RID: 8560
		private DataControlRowType _rowType;

		// Token: 0x04002171 RID: 8561
		private DataControlRowState _rowState;

		// Token: 0x04002172 RID: 8562
		private object _dataItem;
	}
}
