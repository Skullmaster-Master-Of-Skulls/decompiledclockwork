using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005F3 RID: 1523
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ObjectDataSourceDisposingEventArgs : CancelEventArgs
	{
		// Token: 0x06004B90 RID: 19344 RVA: 0x001337BC File Offset: 0x001327BC
		public ObjectDataSourceDisposingEventArgs(object objectInstance)
		{
			this._objectInstance = objectInstance;
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06004B91 RID: 19345 RVA: 0x001337CB File Offset: 0x001327CB
		public object ObjectInstance
		{
			get
			{
				return this._objectInstance;
			}
		}

		// Token: 0x04002BAB RID: 11179
		private object _objectInstance;
	}
}
