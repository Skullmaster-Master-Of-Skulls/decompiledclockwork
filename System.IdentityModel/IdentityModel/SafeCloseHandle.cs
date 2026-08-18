using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.IdentityModel
{
	// Token: 0x02000099 RID: 153
	internal sealed class SafeCloseHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x00006319 File Offset: 0x00004519
		private SafeCloseHandle() : base(true)
		{
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00013000 File Offset: 0x00011200
		internal SafeCloseHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00013010 File Offset: 0x00011210
		protected override bool ReleaseHandle()
		{
			return SafeCloseHandle.CloseHandle(this.handle);
		}

		// Token: 0x0600050B RID: 1291
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x0400045F RID: 1119
		private const string KERNEL32 = "kernel32.dll";
	}
}
