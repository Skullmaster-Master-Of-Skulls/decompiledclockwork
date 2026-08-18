using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020006A5 RID: 1701
	[ComVisible(true)]
	public interface IMessageSink
	{
		// Token: 0x06003D72 RID: 15730
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		IMessage SyncProcessMessage(IMessage msg);

		// Token: 0x06003D73 RID: 15731
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink);

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06003D74 RID: 15732
		IMessageSink NextSink { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; }
	}
}
