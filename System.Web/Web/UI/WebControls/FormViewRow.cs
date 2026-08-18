using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000591 RID: 1425
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class FormViewRow : TableRow
	{
		// Token: 0x060045F0 RID: 17904 RVA: 0x0011EAFA File Offset: 0x0011DAFA
		public FormViewRow(int itemIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			this._itemIndex = itemIndex;
			this._rowType = rowType;
			this._rowState = rowState;
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x060045F1 RID: 17905 RVA: 0x0011EB17 File Offset: 0x0011DB17
		public virtual int ItemIndex
		{
			get
			{
				return this._itemIndex;
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x060045F2 RID: 17906 RVA: 0x0011EB1F File Offset: 0x0011DB1F
		public virtual DataControlRowState RowState
		{
			get
			{
				return this._rowState;
			}
		}

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x060045F3 RID: 17907 RVA: 0x0011EB27 File Offset: 0x0011DB27
		public virtual DataControlRowType RowType
		{
			get
			{
				return this._rowType;
			}
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x0011EB30 File Offset: 0x0011DB30
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

		// Token: 0x04002A24 RID: 10788
		private int _itemIndex;

		// Token: 0x04002A25 RID: 10789
		private DataControlRowType _rowType;

		// Token: 0x04002A26 RID: 10790
		private DataControlRowState _rowState;
	}
}
