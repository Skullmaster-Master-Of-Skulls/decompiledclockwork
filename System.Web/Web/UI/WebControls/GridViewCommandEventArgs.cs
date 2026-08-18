using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200059C RID: 1436
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class GridViewCommandEventArgs : CommandEventArgs
	{
		// Token: 0x060046EC RID: 18156 RVA: 0x00123AE2 File Offset: 0x00122AE2
		public GridViewCommandEventArgs(GridViewRow row, object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this._row = row;
			this._commandSource = commandSource;
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x00123AF9 File Offset: 0x00122AF9
		public GridViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this._commandSource = commandSource;
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x060046EE RID: 18158 RVA: 0x00123B09 File Offset: 0x00122B09
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x060046EF RID: 18159 RVA: 0x00123B11 File Offset: 0x00122B11
		internal GridViewRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x04002A75 RID: 10869
		private GridViewRow _row;

		// Token: 0x04002A76 RID: 10870
		private object _commandSource;
	}
}
