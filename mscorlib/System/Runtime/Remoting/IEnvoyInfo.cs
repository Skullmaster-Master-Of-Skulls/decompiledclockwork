using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace System.Runtime.Remoting
{
	// Token: 0x02000732 RID: 1842
	[ComVisible(true)]
	public interface IEnvoyInfo
	{
		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060041FA RID: 16890
		// (set) Token: 0x060041FB RID: 16891
		IMessageSink EnvoySinks { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] set; }
	}
}
