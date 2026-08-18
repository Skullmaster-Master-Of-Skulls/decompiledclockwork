using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001ED RID: 493
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeSspiAuthDataHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012F9 RID: 4857 RVA: 0x000641DD File Offset: 0x000623DD
		public SafeSspiAuthDataHandle() : base(true)
		{
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x000641E6 File Offset: 0x000623E6
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SspiHelper.SspiFreeAuthIdentity(this.handle) == SecurityStatus.OK;
		}
	}
}
