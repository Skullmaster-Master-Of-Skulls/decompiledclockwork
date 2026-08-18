using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Net.Sockets
{
	// Token: 0x02000393 RID: 915
	// (Invoke) Token: 0x06002255 RID: 8789
	[SuppressUnmanagedCodeSecurity]
	internal delegate bool ConnectExDelegate(SafeCloseSocket socketHandle, IntPtr socketAddress, int socketAddressSize, IntPtr buffer, int dataLength, out int bytesSent, SafeHandle overlapped);
}
