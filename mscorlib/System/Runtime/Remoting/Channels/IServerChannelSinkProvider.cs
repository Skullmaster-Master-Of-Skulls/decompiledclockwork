using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006D4 RID: 1748
	[ComVisible(true)]
	public interface IServerChannelSinkProvider
	{
		// Token: 0x06003F02 RID: 16130
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void GetChannelData(IChannelDataStore channelData);

		// Token: 0x06003F03 RID: 16131
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		IServerChannelSink CreateSink(IChannelReceiver channel);

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06003F04 RID: 16132
		// (set) Token: 0x06003F05 RID: 16133
		IServerChannelSinkProvider Next { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] set; }
	}
}
