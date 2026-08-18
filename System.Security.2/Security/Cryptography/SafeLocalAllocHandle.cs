using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x02000010 RID: 16
	[SecurityCritical]
	internal sealed class SafeLocalAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000042C2 File Offset: 0x000024C2
		private SafeLocalAllocHandle() : base(true)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000042D8 File Offset: 0x000024D8
		internal SafeLocalAllocHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000042E8 File Offset: 0x000024E8
		internal static SafeLocalAllocHandle InvalidHandle
		{
			get
			{
				SafeLocalAllocHandle safeLocalAllocHandle = new SafeLocalAllocHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeLocalAllocHandle);
				return safeLocalAllocHandle;
			}
		}

		// Token: 0x0600007C RID: 124
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x0600007D RID: 125 RVA: 0x00004307 File Offset: 0x00002507
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeLocalAllocHandle.LocalFree(this.handle) == IntPtr.Zero;
		}
	}
}
