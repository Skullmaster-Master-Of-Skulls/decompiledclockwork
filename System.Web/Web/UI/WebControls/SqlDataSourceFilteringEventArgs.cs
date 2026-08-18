using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200064A RID: 1610
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SqlDataSourceFilteringEventArgs : CancelEventArgs
	{
		// Token: 0x06004F38 RID: 20280 RVA: 0x0013F4DC File Offset: 0x0013E4DC
		public SqlDataSourceFilteringEventArgs(IOrderedDictionary parameterValues)
		{
			this._parameterValues = parameterValues;
		}

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x06004F39 RID: 20281 RVA: 0x0013F4EB File Offset: 0x0013E4EB
		public IOrderedDictionary ParameterValues
		{
			get
			{
				return this._parameterValues;
			}
		}

		// Token: 0x04002CD8 RID: 11480
		private IOrderedDictionary _parameterValues;
	}
}
