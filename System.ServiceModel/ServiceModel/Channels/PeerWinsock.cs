using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A4D RID: 2637
	internal static class PeerWinsock
	{
		// Token: 0x06006842 RID: 26690
		[DllImport("ws2_32.dll", SetLastError = true)]
		internal static extern int WSAIoctl([In] IntPtr socketHandle, [In] int ioControlCode, [In] IntPtr inBuffer, [In] int inBufferSize, [Out] IntPtr outBuffer, [In] int outBufferSize, out int bytesTransferred, [In] IntPtr overlapped, [In] IntPtr completionRoutine);
	}
}
