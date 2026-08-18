using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003E7 RID: 999
	public class DetailsViewRow : TableRow
	{
		// Token: 0x0600305E RID: 12382 RVA: 0x0009E5D6 File Offset: 0x0009C7D6
		public DetailsViewRow(int rowIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			this._rowIndex = rowIndex;
			this._rowType = rowType;
			this._rowState = rowState;
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x0009E5F3 File Offset: 0x0009C7F3
		public virtual int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x0009E5FB File Offset: 0x0009C7FB
		public virtual DataControlRowState RowState
		{
			get
			{
				return this._rowState;
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x0009E603 File Offset: 0x0009C803
		public virtual DataControlRowType RowType
		{
			get
			{
				return this._rowType;
			}
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x0009E60C File Offset: 0x0009C80C
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				DetailsViewCommandEventArgs args = new DetailsViewCommandEventArgs(source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}

		// Token: 0x04002086 RID: 8326
		private int _rowIndex;

		// Token: 0x04002087 RID: 8327
		private DataControlRowType _rowType;

		// Token: 0x04002088 RID: 8328
		private DataControlRowState _rowState;
	}
}
