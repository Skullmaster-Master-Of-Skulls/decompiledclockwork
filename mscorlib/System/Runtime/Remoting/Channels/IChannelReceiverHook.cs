using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006E5 RID: 1765
	[ComVisible(true)]
	public interface IChannelReceiverHook
	{
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06003F47 RID: 16199
		string ChannelScheme { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; }

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06003F48 RID: 16200
		bool WantsToListen { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; }

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06003F49 RID: 16201
		IServerChannelSink ChannelSinkChain { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; }

		// Token: 0x06003F4A RID: 16202
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void AddHookChannelUri(string channelUri);
	}
}
