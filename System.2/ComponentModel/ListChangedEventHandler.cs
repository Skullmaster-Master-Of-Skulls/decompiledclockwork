using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000586 RID: 1414
	// (Invoke) Token: 0x0600343D RID: 13373
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void ListChangedEventHandler(object sender, ListChangedEventArgs e);
}
