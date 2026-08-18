using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020007DA RID: 2010
	[Guid("1cc9099d-0a8d-41cb-87d6-845e4f8c4e91")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IPphManager
	{
		// Token: 0x0600603C RID: 24636
		void StartProcessProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, IListenerChannelCallback listenerChannelCallback);

		// Token: 0x0600603D RID: 24637
		void StopProcessProtocolListenerChannel([MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, int listenerChannelId, bool immediate);

		// Token: 0x0600603E RID: 24638
		void StopProcessProtocol([MarshalAs(UnmanagedType.LPWStr)] [In] string protocolId, bool immediate);
	}
}
