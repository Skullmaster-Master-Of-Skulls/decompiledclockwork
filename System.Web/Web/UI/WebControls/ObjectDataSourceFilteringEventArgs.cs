using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005F6 RID: 1526
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ObjectDataSourceFilteringEventArgs : CancelEventArgs
	{
		// Token: 0x06004B99 RID: 19353 RVA: 0x001337F3 File Offset: 0x001327F3
		public ObjectDataSourceFilteringEventArgs(IOrderedDictionary parameterValues)
		{
			this._parameterValues = parameterValues;
		}

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06004B9A RID: 19354 RVA: 0x00133802 File Offset: 0x00132802
		public IOrderedDictionary ParameterValues
		{
			get
			{
				return this._parameterValues;
			}
		}

		// Token: 0x04002BAD RID: 11181
		private IOrderedDictionary _parameterValues;
	}
}
