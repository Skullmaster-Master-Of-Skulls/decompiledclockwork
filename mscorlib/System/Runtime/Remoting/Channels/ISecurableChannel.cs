using System;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000705 RID: 1797
	public interface ISecurableChannel
	{
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06003FEF RID: 16367
		// (set) Token: 0x06003FF0 RID: 16368
		bool IsSecured { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] set; }
	}
}
