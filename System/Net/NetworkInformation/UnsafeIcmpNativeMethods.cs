using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200060F RID: 1551
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeIcmpNativeMethods
	{
		// Token: 0x06002FF4 RID: 12276
		[DllImport("icmp.dll", SetLastError = true)]
		internal static extern SafeCloseIcmpHandle IcmpCreateFile();

		// Token: 0x06002FF5 RID: 12277
		[DllImport("icmp.dll", SetLastError = true)]
		internal static extern bool IcmpCloseHandle(IntPtr icmpHandle);

		// Token: 0x06002FF6 RID: 12278
		[DllImport("icmp.dll", SetLastError = true)]
		internal static extern uint IcmpSendEcho2(SafeCloseIcmpHandle icmpHandle, SafeWaitHandle Event, IntPtr apcRoutine, IntPtr apcContext, uint ipAddress, [In] SafeLocalFree data, ushort dataSize, ref IPOptions options, SafeLocalFree replyBuffer, uint replySize, uint timeout);

		// Token: 0x06002FF7 RID: 12279
		[DllImport("icmp.dll", SetLastError = true)]
		internal static extern uint IcmpSendEcho2(SafeCloseIcmpHandle icmpHandle, IntPtr Event, IntPtr apcRoutine, IntPtr apcContext, uint ipAddress, [In] SafeLocalFree data, ushort dataSize, ref IPOptions options, SafeLocalFree replyBuffer, uint replySize, uint timeout);

		// Token: 0x06002FF8 RID: 12280
		[DllImport("icmp.dll", SetLastError = true)]
		internal static extern uint IcmpParseReplies(IntPtr replyBuffer, uint replySize);

		// Token: 0x04002DCA RID: 11722
		private const string ICMP = "icmp.dll";
	}
}
