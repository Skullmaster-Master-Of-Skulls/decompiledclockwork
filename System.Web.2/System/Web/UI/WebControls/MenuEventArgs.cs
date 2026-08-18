using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200046C RID: 1132
	public sealed class MenuEventArgs : CommandEventArgs
	{
		// Token: 0x06003789 RID: 14217 RVA: 0x000B4951 File Offset: 0x000B2B51
		public MenuEventArgs(MenuItem item, object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this._item = item;
			this._commandSource = commandSource;
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000B4968 File Offset: 0x000B2B68
		public MenuEventArgs(MenuItem item) : this(item, null, new CommandEventArgs(string.Empty, null))
		{
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x0600378B RID: 14219 RVA: 0x000B497D File Offset: 0x000B2B7D
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x0600378C RID: 14220 RVA: 0x000B4985 File Offset: 0x000B2B85
		public MenuItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x0400225E RID: 8798
		private MenuItem _item;

		// Token: 0x0400225F RID: 8799
		private object _commandSource;
	}
}
