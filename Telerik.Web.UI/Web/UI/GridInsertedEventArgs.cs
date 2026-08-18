using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010C6 RID: 4294
	public class GridInsertedEventArgs : GridDataChangeEventArgs
	{
		// Token: 0x0600AF57 RID: 44887 RVA: 0x0025F709 File Offset: 0x0025D909
		public GridInsertedEventArgs(int affectedRows, Exception e, GridEditableItem item) : base(affectedRows, e, item)
		{
		}

		// Token: 0x1700389E RID: 14494
		// (get) Token: 0x0600AF58 RID: 44888 RVA: 0x0025F714 File Offset: 0x0025D914
		// (set) Token: 0x0600AF59 RID: 44889 RVA: 0x0025F71C File Offset: 0x0025D91C
		public bool KeepInInsertMode
		{
			get
			{
				return this._keepInInsertMode;
			}
			set
			{
				this._keepInInsertMode = value;
			}
		}

		// Token: 0x04002E32 RID: 11826
		private bool _keepInInsertMode;
	}
}
