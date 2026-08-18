using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x0200050E RID: 1294
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeAddrInfo : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600281C RID: 10268 RVA: 0x000A57D8 File Offset: 0x000A47D8
		private SafeFreeAddrInfo() : base(true)
		{
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000A57E1 File Offset: 0x000A47E1
		internal static int GetAddrInfo(string nodename, string servicename, ref AddressInfo hints, out SafeFreeAddrInfo outAddrInfo)
		{
			return UnsafeNclNativeMethods.SafeNetHandlesXPOrLater.getaddrinfo(nodename, servicename, ref hints, out outAddrInfo);
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000A57EC File Offset: 0x000A47EC
		protected override bool ReleaseHandle()
		{
			UnsafeNclNativeMethods.SafeNetHandlesXPOrLater.freeaddrinfo(this.handle);
			return true;
		}

		// Token: 0x04002769 RID: 10089
		private const string WS2_32 = "ws2_32.dll";
	}
}
