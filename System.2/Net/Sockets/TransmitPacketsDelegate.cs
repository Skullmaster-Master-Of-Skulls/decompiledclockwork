using System;
using System.Security;

namespace System.Net.Sockets
{
	// Token: 0x02000398 RID: 920
	// (Invoke) Token: 0x06002269 RID: 8809
	[SuppressUnmanagedCodeSecurity]
	internal delegate bool TransmitPacketsDelegate(SafeCloseSocket socketHandle, IntPtr packetArray, int elementCount, int sendSize, SafeNativeOverlapped overlapped, TransmitFileOptions flags);
}
