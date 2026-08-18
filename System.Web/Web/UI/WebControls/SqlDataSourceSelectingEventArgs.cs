using System;
using System.Data.Common;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200064D RID: 1613
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SqlDataSourceSelectingEventArgs : SqlDataSourceCommandEventArgs
	{
		// Token: 0x06004F3E RID: 20286 RVA: 0x0013F4F3 File Offset: 0x0013E4F3
		public SqlDataSourceSelectingEventArgs(DbCommand command, DataSourceSelectArguments arguments) : base(command)
		{
			this._arguments = arguments;
		}

		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x06004F3F RID: 20287 RVA: 0x0013F503 File Offset: 0x0013E503
		public DataSourceSelectArguments Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x04002CDC RID: 11484
		private DataSourceSelectArguments _arguments;
	}
}
