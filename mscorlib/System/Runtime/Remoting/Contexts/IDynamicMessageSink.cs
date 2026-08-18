using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020006FC RID: 1788
	[ComVisible(true)]
	public interface IDynamicMessageSink
	{
		// Token: 0x06003F9B RID: 16283
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void ProcessMessageStart(IMessage reqMsg, bool bCliSide, bool bAsync);

		// Token: 0x06003F9C RID: 16284
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void ProcessMessageFinish(IMessage replyMsg, bool bCliSide, bool bAsync);
	}
}
