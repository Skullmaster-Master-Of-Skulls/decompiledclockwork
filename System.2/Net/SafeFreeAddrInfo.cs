using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001E7 RID: 487
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeAddrInfo : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012E8 RID: 4840 RVA: 0x000640C8 File Offset: 0x000622C8
		private SafeFreeAddrInfo() : base(true)
		{
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000640D1 File Offset: 0x000622D1
		internal static int GetAddrInfo(string nodename, string servicename, ref AddressInfo hints, out SafeFreeAddrInfo outAddrInfo)
		{
			return UnsafeNclNativeMethods.SafeNetHandlesXPOrLater.GetAddrInfoW(nodename, servicename, ref hints, out outAddrInfo);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x000640DC File Offset: 0x000622DC
		protected override bool ReleaseHandle()
		{
			UnsafeNclNativeMethods.SafeNetHandlesXPOrLater.freeaddrinfo(this.handle);
			return true;
		}

		// Token: 0x04001535 RID: 5429
		private const string WS2_32 = "ws2_32.dll";
	}
}
