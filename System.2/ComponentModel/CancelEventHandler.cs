using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000522 RID: 1314
	// (Invoke) Token: 0x060031DE RID: 12766
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void CancelEventHandler(object sender, CancelEventArgs e);
}
