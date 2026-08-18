using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x020006BD RID: 1725
	[ComVisible(true)]
	public interface ISponsor
	{
		// Token: 0x06003E14 RID: 15892
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		TimeSpan Renewal(ILease lease);
	}
}
