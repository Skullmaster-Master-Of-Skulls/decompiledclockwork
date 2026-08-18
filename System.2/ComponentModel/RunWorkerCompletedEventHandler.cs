using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005A9 RID: 1449
	// (Invoke) Token: 0x06003619 RID: 13849
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void RunWorkerCompletedEventHandler(object sender, RunWorkerCompletedEventArgs e);
}
