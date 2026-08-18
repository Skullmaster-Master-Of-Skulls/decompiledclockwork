using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003C7 RID: 967
	public class DataGridSortCommandEventArgs : EventArgs
	{
		// Token: 0x06002E92 RID: 11922 RVA: 0x000988B5 File Offset: 0x00096AB5
		public DataGridSortCommandEventArgs(object commandSource, DataGridCommandEventArgs dce)
		{
			this.commandSource = commandSource;
			this.sortExpression = (string)dce.CommandArgument;
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x000988D5 File Offset: 0x00096AD5
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x000988DD File Offset: 0x00096ADD
		public string SortExpression
		{
			get
			{
				return this.sortExpression;
			}
		}

		// Token: 0x04001FFC RID: 8188
		private string sortExpression;

		// Token: 0x04001FFD RID: 8189
		private object commandSource;
	}
}
