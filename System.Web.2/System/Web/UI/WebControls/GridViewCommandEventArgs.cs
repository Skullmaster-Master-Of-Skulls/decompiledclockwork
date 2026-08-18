using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000418 RID: 1048
	public class GridViewCommandEventArgs : CommandEventArgs
	{
		// Token: 0x0600336B RID: 13163 RVA: 0x000A8EF2 File Offset: 0x000A70F2
		public GridViewCommandEventArgs(GridViewRow row, object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this._row = row;
			this._commandSource = commandSource;
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000A8F09 File Offset: 0x000A7109
		public GridViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this._commandSource = commandSource;
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x000A8F19 File Offset: 0x000A7119
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x000A8F21 File Offset: 0x000A7121
		// (set) Token: 0x0600336F RID: 13167 RVA: 0x000A8F29 File Offset: 0x000A7129
		public bool Handled { get; set; }

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x000A8F32 File Offset: 0x000A7132
		internal GridViewRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x04002161 RID: 8545
		private GridViewRow _row;

		// Token: 0x04002162 RID: 8546
		private object _commandSource;
	}
}
