using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000613 RID: 1555
	// (Invoke) Token: 0x060038F1 RID: 14577
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void ResolveNameEventHandler(object sender, ResolveNameEventArgs e);
}
