using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020002B4 RID: 692
	[Guid("1cc9099d-0a8d-41cb-87d6-845e4f8c4e91")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[ComImport]
	public interface IPphManager
	{
		// Token: 0x060023FC RID: 9212
		void StartProcessProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, IListenerChannelCallback listenerChannelCallback);

		// Token: 0x060023FD RID: 9213
		void StopProcessProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, int listenerChannelId, bool immediate);

		// Token: 0x060023FE RID: 9214
		void StopProcessProtocol([MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, bool immediate);
	}
}
