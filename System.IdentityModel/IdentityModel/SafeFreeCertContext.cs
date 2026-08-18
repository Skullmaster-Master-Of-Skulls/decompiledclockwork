using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.IdentityModel
{
	// Token: 0x02000097 RID: 151
	internal sealed class SafeFreeCertContext : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x00006319 File Offset: 0x00004519
		internal SafeFreeCertContext() : base(true)
		{
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00012ED4 File Offset: 0x000110D4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x060004FD RID: 1277
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern bool CertFreeCertificateContext([In] IntPtr certContext);

		// Token: 0x060004FE RID: 1278 RVA: 0x00012EDD File Offset: 0x000110DD
		protected override bool ReleaseHandle()
		{
			return SafeFreeCertContext.CertFreeCertificateContext(this.handle);
		}

		// Token: 0x0400045B RID: 1115
		private const string CRYPT32 = "crypt32.dll";

		// Token: 0x0400045C RID: 1116
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x0400045D RID: 1117
		private const uint CRYPT_ACQUIRE_SILENT_FLAG = 64U;
	}
}
