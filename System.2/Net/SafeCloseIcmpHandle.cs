using System;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001EB RID: 491
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeCloseIcmpHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012F5 RID: 4853 RVA: 0x000641B1 File Offset: 0x000623B1
		private SafeCloseIcmpHandle() : base(true)
		{
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000641BA File Offset: 0x000623BA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected override bool ReleaseHandle()
		{
			return UnsafeNetInfoNativeMethods.IcmpCloseHandle(this.handle);
		}
	}
}
