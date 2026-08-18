using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000610 RID: 1552
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeWinINetNativeMethods
	{
		// Token: 0x06002FF9 RID: 12281
		[DllImport("wininet.dll")]
		internal static extern bool InternetGetConnectedState(ref uint flags, uint dwReserved);

		// Token: 0x04002DCB RID: 11723
		private const string WININET = "wininet.dll";
	}
}
