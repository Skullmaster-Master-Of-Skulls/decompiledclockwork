using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000424 RID: 1060
	public class GridViewRowEventArgs : EventArgs
	{
		// Token: 0x060033AE RID: 13230 RVA: 0x000A917D File Offset: 0x000A737D
		public GridViewRowEventArgs(GridViewRow row)
		{
			this._row = row;
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x060033AF RID: 13231 RVA: 0x000A918C File Offset: 0x000A738C
		public GridViewRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x04002174 RID: 8564
		private GridViewRow _row;
	}
}
