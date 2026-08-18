using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006CE RID: 1742
	[ComVisible(true)]
	public interface IChannelReceiver : IChannel
	{
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06003ED4 RID: 16084
		object ChannelData { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; }

		// Token: 0x06003ED5 RID: 16085
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		string[] GetUrlsForUri(string objectURI);

		// Token: 0x06003ED6 RID: 16086
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void StartListening(object data);

		// Token: 0x06003ED7 RID: 16087
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void StopListening(object data);
	}
}
