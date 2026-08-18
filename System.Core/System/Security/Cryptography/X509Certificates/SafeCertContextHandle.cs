using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000135 RID: 309
	internal sealed class SafeCertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000A0B RID: 2571 RVA: 0x000246A9 File Offset: 0x000228A9
		[SecuritySafeCritical]
		private SafeCertContextHandle() : base(true)
		{
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000246B2 File Offset: 0x000228B2
		[SecuritySafeCritical]
		internal SafeCertContextHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x000246C4 File Offset: 0x000228C4
		internal static SafeCertContextHandle InvalidHandle
		{
			[SecuritySafeCritical]
			get
			{
				SafeCertContextHandle safeCertContextHandle = new SafeCertContextHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertContextHandle);
				return safeCertContextHandle;
			}
		}

		// Token: 0x06000A0E RID: 2574
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern bool CertFreeCertificateContext(IntPtr pCertContext);

		// Token: 0x06000A0F RID: 2575 RVA: 0x000246E3 File Offset: 0x000228E3
		[SecuritySafeCritical]
		protected override bool ReleaseHandle()
		{
			return SafeCertContextHandle.CertFreeCertificateContext(this.handle);
		}
	}
}
