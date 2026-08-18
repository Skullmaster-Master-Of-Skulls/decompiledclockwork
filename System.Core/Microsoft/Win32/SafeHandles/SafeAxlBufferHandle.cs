using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000017 RID: 23
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeAxlBufferHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000CD RID: 205 RVA: 0x000033A7 File Offset: 0x000015A7
		private SafeAxlBufferHandle() : base(true)
		{
		}

		// Token: 0x060000CE RID: 206
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32")]
		private static extern IntPtr GetProcessHeap();

		// Token: 0x060000CF RID: 207
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool HeapFree(IntPtr hHeap, int dwFlags, IntPtr lpMem);

		// Token: 0x060000D0 RID: 208 RVA: 0x000033B0 File Offset: 0x000015B0
		protected override bool ReleaseHandle()
		{
			SafeAxlBufferHandle.HeapFree(SafeAxlBufferHandle.GetProcessHeap(), 0, this.handle);
			return true;
		}
	}
}
