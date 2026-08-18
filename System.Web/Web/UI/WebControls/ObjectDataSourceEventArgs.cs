using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005F5 RID: 1525
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ObjectDataSourceEventArgs : EventArgs
	{
		// Token: 0x06004B96 RID: 19350 RVA: 0x001337D3 File Offset: 0x001327D3
		public ObjectDataSourceEventArgs(object objectInstance)
		{
			this._objectInstance = objectInstance;
		}

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06004B97 RID: 19351 RVA: 0x001337E2 File Offset: 0x001327E2
		// (set) Token: 0x06004B98 RID: 19352 RVA: 0x001337EA File Offset: 0x001327EA
		public object ObjectInstance
		{
			get
			{
				return this._objectInstance;
			}
			set
			{
				this._objectInstance = value;
			}
		}

		// Token: 0x04002BAC RID: 11180
		private object _objectInstance;
	}
}
