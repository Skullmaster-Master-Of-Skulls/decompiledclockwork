using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200040F RID: 1039
	public class FormViewRow : TableRow
	{
		// Token: 0x06003238 RID: 12856 RVA: 0x000A3906 File Offset: 0x000A1B06
		public FormViewRow(int itemIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			this._itemIndex = itemIndex;
			this._rowType = rowType;
			this._rowState = rowState;
			this.RenderTemplateContainer = true;
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x000A392A File Offset: 0x000A1B2A
		public virtual int ItemIndex
		{
			get
			{
				return this._itemIndex;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x000A3932 File Offset: 0x000A1B32
		public virtual DataControlRowState RowState
		{
			get
			{
				return this._rowState;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x000A393A File Offset: 0x000A1B3A
		public virtual DataControlRowType RowType
		{
			get
			{
				return this._rowType;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x000A3942 File Offset: 0x000A1B42
		// (set) Token: 0x0600323D RID: 12861 RVA: 0x000A394A File Offset: 0x000A1B4A
		internal bool RenderTemplateContainer { get; set; }

		// Token: 0x0600323E RID: 12862 RVA: 0x000A3954 File Offset: 0x000A1B54
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.RenderTemplateContainer)
			{
				base.Render(writer);
				return;
			}
			foreach (object obj in this.Cells)
			{
				TableCell tableCell = (TableCell)obj;
				tableCell.RenderContents(writer);
			}
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000A39C0 File Offset: 0x000A1BC0
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				FormViewCommandEventArgs args = new FormViewCommandEventArgs(source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}

		// Token: 0x04002108 RID: 8456
		private int _itemIndex;

		// Token: 0x04002109 RID: 8457
		private DataControlRowType _rowType;

		// Token: 0x0400210A RID: 8458
		private DataControlRowState _rowState;
	}
}
