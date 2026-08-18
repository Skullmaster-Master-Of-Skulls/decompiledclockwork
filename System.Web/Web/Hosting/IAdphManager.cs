using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020002B3 RID: 691
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public interface IAdphManager
	{
		// Token: 0x060023F9 RID: 9209
		void StartAppDomainProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, IListenerChannelCallback listenerChannelCallback);

		// Token: 0x060023FA RID: 9210
		void StopAppDomainProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, int listenerChannelId, bool immediate);

		// Token: 0x060023FB RID: 9211
		void StopAppDomainProtocol([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, bool immediate);
	}
}
