using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005A6 RID: 1446
	// (Invoke) Token: 0x0600360C RID: 13836
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void RefreshEventHandler(RefreshEventArgs e);
}
