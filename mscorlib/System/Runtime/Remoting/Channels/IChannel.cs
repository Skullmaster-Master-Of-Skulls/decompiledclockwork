using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006CC RID: 1740
	[ComVisible(true)]
	public interface IChannel
	{
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06003ED0 RID: 16080
		int ChannelPriority { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; }

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06003ED1 RID: 16081
		string ChannelName { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; }

		// Token: 0x06003ED2 RID: 16082
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		string Parse(string url, out string objectURI);
	}
}
