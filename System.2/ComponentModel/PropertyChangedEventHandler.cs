using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000599 RID: 1433
	// (Invoke) Token: 0x06003531 RID: 13617
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
}
