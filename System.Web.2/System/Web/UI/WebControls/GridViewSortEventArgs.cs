using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000428 RID: 1064
	public class GridViewSortEventArgs : CancelEventArgs
	{
		// Token: 0x060033BB RID: 13243 RVA: 0x000A91B4 File Offset: 0x000A73B4
		public GridViewSortEventArgs(string sortExpression, SortDirection sortDirection)
		{
			this._sortExpression = sortExpression;
			this._sortDirection = sortDirection;
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x060033BC RID: 13244 RVA: 0x000A91CA File Offset: 0x000A73CA
		// (set) Token: 0x060033BD RID: 13245 RVA: 0x000A91D2 File Offset: 0x000A73D2
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

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x060033BE RID: 13246 RVA: 0x000A91DB File Offset: 0x000A73DB
		// (set) Token: 0x060033BF RID: 13247 RVA: 0x000A91E3 File Offset: 0x000A73E3
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

		// Token: 0x04002176 RID: 8566
		private string _sortExpression;

		// Token: 0x04002177 RID: 8567
		private SortDirection _sortDirection;
	}
}
