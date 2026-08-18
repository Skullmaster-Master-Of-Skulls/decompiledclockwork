using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting
{
	// Token: 0x02000730 RID: 1840
	[ComVisible(true)]
	public interface IRemotingTypeInfo
	{
		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060041F5 RID: 16885
		// (set) Token: 0x060041F6 RID: 16886
		string TypeName { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] set; }

		// Token: 0x060041F7 RID: 16887
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		bool CanCastTo(Type fromType, object o);
	}
}
