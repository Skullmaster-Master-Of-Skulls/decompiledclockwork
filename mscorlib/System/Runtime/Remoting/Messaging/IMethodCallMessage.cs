using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020006E1 RID: 1761
	[ComVisible(true)]
	public interface IMethodCallMessage : IMethodMessage, IMessage
	{
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06003F37 RID: 16183
		int InArgCount { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; }

		// Token: 0x06003F38 RID: 16184
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		string GetInArgName(int index);

		// Token: 0x06003F39 RID: 16185
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		object GetInArg(int argNum);

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06003F3A RID: 16186
		object[] InArgs { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; }
	}
}
