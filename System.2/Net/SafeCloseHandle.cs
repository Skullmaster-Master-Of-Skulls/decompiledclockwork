using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001E8 RID: 488
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeCloseHandle : CriticalHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012EB RID: 4843 RVA: 0x000640EA File Offset: 0x000622EA
		private SafeCloseHandle()
		{
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x000640F2 File Offset: 0x000622F2
		internal IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x000640FA File Offset: 0x000622FA
		protected override bool ReleaseHandle()
		{
			return this.IsInvalid || Interlocked.Increment(ref this._disposed) != 1 || UnsafeNclNativeMethods.SafeNetHandles.CloseHandle(this.handle);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0006411F File Offset: 0x0006231F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Abort()
		{
			this.ReleaseHandle();
			base.SetHandleAsInvalid();
		}

		// Token: 0x04001536 RID: 5430
		private int _disposed;
	}
}
