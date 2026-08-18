using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005DF RID: 1503
	// (Invoke) Token: 0x060037D6 RID: 14294
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void DesignerEventHandler(object sender, DesignerEventArgs e);
}
