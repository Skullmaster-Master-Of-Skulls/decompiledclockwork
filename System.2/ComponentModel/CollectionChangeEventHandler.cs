using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000527 RID: 1319
	// (Invoke) Token: 0x060031FE RID: 12798
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void CollectionChangeEventHandler(object sender, CollectionChangeEventArgs e);
}
