using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x0200051C RID: 1308
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeCertContext : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600284E RID: 10318 RVA: 0x000A5F00 File Offset: 0x000A4F00
		internal SafeFreeCertContext() : base(true)
		{
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000A5F09 File Offset: 0x000A4F09
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000A5F12 File Offset: 0x000A4F12
		protected override bool ReleaseHandle()
		{
			UnsafeNclNativeMethods.SafeNetHandles.CertFreeCertificateContext(this.handle);
			return true;
		}

		// Token: 0x0400277F RID: 10111
		private const string CRYPT32 = "crypt32.dll";

		// Token: 0x04002780 RID: 10112
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x04002781 RID: 10113
		private const uint CRYPT_ACQUIRE_SILENT_FLAG = 64U;
	}
}
