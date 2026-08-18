using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x0200079A RID: 1946
	[Guid("dc3b0a85-9da7-47e4-ba1b-e27da9db8a1e")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IListenerChannelCallback
	{
		// Token: 0x06005CA4 RID: 23716
		void ReportStarted();

		// Token: 0x06005CA5 RID: 23717
		void ReportStopped(int hr);

		// Token: 0x06005CA6 RID: 23718
		void ReportMessageReceived();

		// Token: 0x06005CA7 RID: 23719
		int GetId();

		// Token: 0x06005CA8 RID: 23720
		int GetBlobLength();

		// Token: 0x06005CA9 RID: 23721
		void GetBlob([MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] buffer, ref int bufferSize);
	}
}
