using System;
using System.Security;

namespace System.Net.Sockets
{
	// Token: 0x02000395 RID: 917
	// (Invoke) Token: 0x0600225D RID: 8797
	[SuppressUnmanagedCodeSecurity]
	internal delegate bool DisconnectExDelegate_Blocking(IntPtr socketHandle, IntPtr overlapped, int flags, int reserved);
}
