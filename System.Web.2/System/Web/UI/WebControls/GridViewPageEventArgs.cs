using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000420 RID: 1056
	public class GridViewPageEventArgs : CancelEventArgs
	{
		// Token: 0x06003391 RID: 13201 RVA: 0x000A902E File Offset: 0x000A722E
		public GridViewPageEventArgs(int newPageIndex)
		{
			this._newPageIndex = newPageIndex;
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x000A903D File Offset: 0x000A723D
		// (set) Token: 0x06003393 RID: 13203 RVA: 0x000A9045 File Offset: 0x000A7245
		public int NewPageIndex
		{
			get
			{
				return this._newPageIndex;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._newPageIndex = value;
			}
		}

		// Token: 0x0400216D RID: 8557
		private int _newPageIndex;
	}
}
