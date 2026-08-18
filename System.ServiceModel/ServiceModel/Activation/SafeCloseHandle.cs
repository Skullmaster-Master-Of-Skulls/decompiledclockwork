using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005CC RID: 1484
	internal sealed class SafeCloseHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060039B6 RID: 14774 RVA: 0x000DEBD4 File Offset: 0x000DCDD4
		private SafeCloseHandle() : base(true)
		{
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000DEBDD File Offset: 0x000DCDDD
		internal SafeCloseHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x000DEBED File Offset: 0x000DCDED
		protected override bool ReleaseHandle()
		{
			return SafeCloseHandle.CloseHandle(this.handle);
		}

		// Token: 0x060039B9 RID: 14777
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x04002A2E RID: 10798
		private const string KERNEL32 = "kernel32.dll";
	}
}
