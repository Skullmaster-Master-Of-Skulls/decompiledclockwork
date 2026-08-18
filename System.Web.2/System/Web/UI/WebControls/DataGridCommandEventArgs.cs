using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003BD RID: 957
	public class DataGridCommandEventArgs : CommandEventArgs
	{
		// Token: 0x06002E56 RID: 11862 RVA: 0x00098193 File Offset: 0x00096393
		public DataGridCommandEventArgs(DataGridItem item, object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this.item = item;
			this.commandSource = commandSource;
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x000981AA File Offset: 0x000963AA
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000981B2 File Offset: 0x000963B2
		public DataGridItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04001FEB RID: 8171
		private DataGridItem item;

		// Token: 0x04001FEC RID: 8172
		private object commandSource;
	}
}
