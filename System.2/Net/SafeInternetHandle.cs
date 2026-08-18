using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001EC RID: 492
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeInternetHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x000641C7 File Offset: 0x000623C7
		public SafeInternetHandle() : base(true)
		{
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000641D0 File Offset: 0x000623D0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.WinHttp.WinHttpCloseHandle(this.handle);
		}
	}
}
