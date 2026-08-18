using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000021 RID: 33
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public abstract class DataBindingHandler
	{
		// Token: 0x06000102 RID: 258
		public abstract void DataBindControl(IDesignerHost designerHost, Control control);
	}
}
