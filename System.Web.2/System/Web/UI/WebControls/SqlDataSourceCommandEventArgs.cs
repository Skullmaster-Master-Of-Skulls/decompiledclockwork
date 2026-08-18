using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004D1 RID: 1233
	public class SqlDataSourceCommandEventArgs : CancelEventArgs
	{
		// Token: 0x06003D8B RID: 15755 RVA: 0x000C652C File Offset: 0x000C472C
		public SqlDataSourceCommandEventArgs(DbCommand command)
		{
			this._command = command;
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x000C653B File Offset: 0x000C473B
		public DbCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x040023C1 RID: 9153
		private DbCommand _command;
	}
}
