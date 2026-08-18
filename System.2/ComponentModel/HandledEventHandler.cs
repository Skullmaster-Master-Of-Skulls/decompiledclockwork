using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000557 RID: 1367
	// (Invoke) Token: 0x06003364 RID: 13156
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void HandledEventHandler(object sender, HandledEventArgs e);
}
