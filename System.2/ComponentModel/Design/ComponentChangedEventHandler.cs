using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005CD RID: 1485
	// (Invoke) Token: 0x06003771 RID: 14193
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void ComponentChangedEventHandler(object sender, ComponentChangedEventArgs e);
}
