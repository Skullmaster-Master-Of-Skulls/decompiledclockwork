using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003C1 RID: 961
	public class DataGridItemEventArgs : EventArgs
	{
		// Token: 0x06002E70 RID: 11888 RVA: 0x000982BD File Offset: 0x000964BD
		public DataGridItemEventArgs(DataGridItem item)
		{
			this.item = item;
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x000982CC File Offset: 0x000964CC
		public DataGridItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04001FF2 RID: 8178
		private DataGridItem item;
	}
}
