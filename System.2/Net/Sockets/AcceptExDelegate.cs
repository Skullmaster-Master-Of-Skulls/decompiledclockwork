using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Net.Sockets
{
	// Token: 0x02000391 RID: 913
	// (Invoke) Token: 0x0600224D RID: 8781
	[SuppressUnmanagedCodeSecurity]
	internal delegate bool AcceptExDelegate(SafeCloseSocket listenSocketHandle, SafeCloseSocket acceptSocketHandle, IntPtr buffer, int len, int localAddressLength, int remoteAddressLength, out int bytesReceived, SafeHandle overlapped);
}
