using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006B9 RID: 1721
	[ComVisible(true)]
	public interface IServerChannelSinkStack : IServerResponseChannelSinkStack
	{
		// Token: 0x06003E03 RID: 15875
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void Push(IServerChannelSink sink, object state);

		// Token: 0x06003E04 RID: 15876
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		object Pop(IServerChannelSink sink);

		// Token: 0x06003E05 RID: 15877
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void Store(IServerChannelSink sink, object state);

		// Token: 0x06003E06 RID: 15878
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void StoreAndDispatch(IServerChannelSink sink, object state);

		// Token: 0x06003E07 RID: 15879
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void ServerCallback(IAsyncResult ar);
	}
}
