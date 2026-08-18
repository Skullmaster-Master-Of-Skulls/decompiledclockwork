using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005E0 RID: 1504
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class MenuEventArgs : CommandEventArgs
	{
		// Token: 0x06004A2C RID: 18988 RVA: 0x0012F24E File Offset: 0x0012E24E
		public MenuEventArgs(MenuItem item, object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this._item = item;
			this._commandSource = commandSource;
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x0012F265 File Offset: 0x0012E265
		public MenuEventArgs(MenuItem item) : this(item, null, new CommandEventArgs(string.Empty, null))
		{
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06004A2E RID: 18990 RVA: 0x0012F27A File Offset: 0x0012E27A
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06004A2F RID: 18991 RVA: 0x0012F282 File Offset: 0x0012E282
		public MenuItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x04002B6B RID: 11115
		private MenuItem _item;

		// Token: 0x04002B6C RID: 11116
		private object _commandSource;
	}
}
