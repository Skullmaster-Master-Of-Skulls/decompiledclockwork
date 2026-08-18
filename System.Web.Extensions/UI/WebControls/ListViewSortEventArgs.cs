using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000B8 RID: 184
	public class ListViewSortEventArgs : CancelEventArgs
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x000226DE File Offset: 0x000208DE
		public ListViewSortEventArgs(string sortExpression, SortDirection sortDirection) : base(false)
		{
			this._sortExpression = sortExpression;
			this._sortDirection = sortDirection;
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x000226F5 File Offset: 0x000208F5
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x000226FD File Offset: 0x000208FD
		public SortDirection SortDirection
		{
			get
			{
				return this._sortDirection;
			}
			set
			{
				this._sortDirection = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x00022706 File Offset: 0x00020906
		// (set) Token: 0x060008F5 RID: 2293 RVA: 0x0002270E File Offset: 0x0002090E
		public string SortExpression
		{
			get
			{
				return this._sortExpression;
			}
			set
			{
				this._sortExpression = value;
			}
		}

		// Token: 0x040002F8 RID: 760
		private string _sortExpression;

		// Token: 0x040002F9 RID: 761
		private SortDirection _sortDirection;
	}
}
