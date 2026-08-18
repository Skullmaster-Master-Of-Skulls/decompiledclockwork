using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F8 RID: 504
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeCertContext : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001327 RID: 4903 RVA: 0x00064914 File Offset: 0x00062B14
		internal SafeFreeCertContext() : base(true)
		{
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0006491D File Offset: 0x00062B1D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00064926 File Offset: 0x00062B26
		protected override bool ReleaseHandle()
		{
			UnsafeNclNativeMethods.SafeNetHandles.CertFreeCertificateContext(this.handle);
			return true;
		}

		// Token: 0x0400154F RID: 5455
		private const string CRYPT32 = "crypt32.dll";

		// Token: 0x04001550 RID: 5456
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x04001551 RID: 5457
		private const uint CRYPT_ACQUIRE_SILENT_FLAG = 64U;
	}
}
