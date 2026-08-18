using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000567 RID: 1383
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DetailsViewRow : TableRow
	{
		// Token: 0x06004433 RID: 17459 RVA: 0x0011992A File Offset: 0x0011892A
		public DetailsViewRow(int rowIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			this._rowIndex = rowIndex;
			this._rowType = rowType;
			this._rowState = rowState;
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x00119947 File Offset: 0x00118947
		public virtual int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06004435 RID: 17461 RVA: 0x0011994F File Offset: 0x0011894F
		public virtual DataControlRowState RowState
		{
			get
			{
				return this._rowState;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06004436 RID: 17462 RVA: 0x00119957 File Offset: 0x00118957
		public virtual DataControlRowType RowType
		{
			get
			{
				return this._rowType;
			}
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x00119960 File Offset: 0x00118960
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

		// Token: 0x040029A3 RID: 10659
		private int _rowIndex;

		// Token: 0x040029A4 RID: 10660
		private DataControlRowType _rowType;

		// Token: 0x040029A5 RID: 10661
		private DataControlRowState _rowState;
	}
}
