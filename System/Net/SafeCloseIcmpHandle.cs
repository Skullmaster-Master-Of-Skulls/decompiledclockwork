using System;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000510 RID: 1296
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeCloseIcmpHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002825 RID: 10277 RVA: 0x000A58E3 File Offset: 0x000A48E3
		private SafeCloseIcmpHandle() : base(true)
		{
			this.IsPostWin2K = ComNetOS.IsPostWin2K;
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x000A58F7 File Offset: 0x000A48F7
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected override bool ReleaseHandle()
		{
			if (this.IsPostWin2K)
			{
				return UnsafeNetInfoNativeMethods.IcmpCloseHandle(this.handle);
			}
			return UnsafeIcmpNativeMethods.IcmpCloseHandle(this.handle);
		}

		// Token: 0x0400276E RID: 10094
		private bool IsPostWin2K;
	}
}
