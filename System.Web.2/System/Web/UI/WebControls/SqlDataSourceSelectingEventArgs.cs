using System;
using System.Data.Common;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004D7 RID: 1239
	public class SqlDataSourceSelectingEventArgs : SqlDataSourceCommandEventArgs
	{
		// Token: 0x06003D97 RID: 15767 RVA: 0x000C655A File Offset: 0x000C475A
		public SqlDataSourceSelectingEventArgs(DbCommand command, DataSourceSelectArguments arguments) : base(command)
		{
			this._arguments = arguments;
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06003D98 RID: 15768 RVA: 0x000C656A File Offset: 0x000C476A
		public DataSourceSelectArguments Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x040023C9 RID: 9161
		private DataSourceSelectArguments _arguments;
	}
}
