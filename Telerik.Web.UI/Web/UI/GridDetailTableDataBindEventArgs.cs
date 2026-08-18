using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010DC RID: 4316
	public class GridDetailTableDataBindEventArgs : EventArgs, IGridCommandEvent
	{
		// Token: 0x17003946 RID: 14662
		// (get) Token: 0x0600B0CD RID: 45261 RVA: 0x0026415F File Offset: 0x0026235F
		public GridTableView DetailTableView
		{
			get
			{
				return this._detailTableView;
			}
		}

		// Token: 0x0600B0CE RID: 45262 RVA: 0x00264167 File Offset: 0x00262367
		public GridDetailTableDataBindEventArgs(object commandSource, GridTableView detailTableView)
		{
			this.commandSource = commandSource;
			this._detailTableView = detailTableView;
		}

		// Token: 0x17003947 RID: 14663
		// (get) Token: 0x0600B0CF RID: 45263 RVA: 0x0026417D File Offset: 0x0026237D
		// (set) Token: 0x0600B0D0 RID: 45264 RVA: 0x00264185 File Offset: 0x00262385
		public bool Canceled
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		// Token: 0x0600B0D1 RID: 45265 RVA: 0x0026418E File Offset: 0x0026238E
		public void ExecuteCommand(object source)
		{
			this.DetailTableView.OwnerGrid.CallOnDetailTableDataBind(this);
		}

		// Token: 0x04002E6D RID: 11885
		private bool _cancel;

		// Token: 0x04002E6E RID: 11886
		private GridTableView _detailTableView;

		// Token: 0x04002E6F RID: 11887
		private object commandSource;
	}
}
