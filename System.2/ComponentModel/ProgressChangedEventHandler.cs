using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000597 RID: 1431
	// (Invoke) Token: 0x0600352B RID: 13611
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);
}
