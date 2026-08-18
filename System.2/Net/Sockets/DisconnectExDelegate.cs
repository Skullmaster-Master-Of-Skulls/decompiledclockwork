using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Net.Sockets
{
	// Token: 0x02000394 RID: 916
	// (Invoke) Token: 0x06002259 RID: 8793
	[SuppressUnmanagedCodeSecurity]
	internal delegate bool DisconnectExDelegate(SafeCloseSocket socketHandle, SafeHandle overlapped, int flags, int reserved);
}
