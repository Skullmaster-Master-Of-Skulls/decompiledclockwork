using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020007D9 RID: 2009
	public interface IAdphManager
	{
		// Token: 0x06006039 RID: 24633
		void StartAppDomainProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, IListenerChannelCallback listenerChannelCallback);

		// Token: 0x0600603A RID: 24634
		void StopAppDomainProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, int listenerChannelId, bool immediate);

		// Token: 0x0600603B RID: 24635
		void StopAppDomainProtocol([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, bool immediate);
	}
}
