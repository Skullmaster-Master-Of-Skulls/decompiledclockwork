using System;
using System.ComponentModel;
using System.Data.Common;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000647 RID: 1607
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SqlDataSourceCommandEventArgs : CancelEventArgs
	{
		// Token: 0x06004F32 RID: 20274 RVA: 0x0013F4C5 File Offset: 0x0013E4C5
		public SqlDataSourceCommandEventArgs(DbCommand command)
		{
			this._command = command;
		}

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x06004F33 RID: 20275 RVA: 0x0013F4D4 File Offset: 0x0013E4D4
		public DbCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x04002CD4 RID: 11476
		private DbCommand _command;
	}
}
