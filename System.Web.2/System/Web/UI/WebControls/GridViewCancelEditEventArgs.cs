using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000416 RID: 1046
	public class GridViewCancelEditEventArgs : CancelEventArgs
	{
		// Token: 0x06003365 RID: 13157 RVA: 0x000A8EDB File Offset: 0x000A70DB
		public GridViewCancelEditEventArgs(int rowIndex)
		{
			this._rowIndex = rowIndex;
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06003366 RID: 13158 RVA: 0x000A8EEA File Offset: 0x000A70EA
		public int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
		}

		// Token: 0x04002160 RID: 8544
		private int _rowIndex;
	}
}
