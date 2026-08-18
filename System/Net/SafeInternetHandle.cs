using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000511 RID: 1297
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeInternetHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002827 RID: 10279 RVA: 0x000A5918 File Offset: 0x000A4918
		public SafeInternetHandle() : base(true)
		{
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x000A5921 File Offset: 0x000A4921
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.WinHttp.WinHttpCloseHandle(this.handle);
		}
	}
}
