using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x02000012 RID: 18
	[SecurityCritical]
	internal sealed class SafeCertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000087 RID: 135 RVA: 0x000042C2 File Offset: 0x000024C2
		private SafeCertContextHandle() : base(true)
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000042D8 File Offset: 0x000024D8
		internal SafeCertContextHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000043F4 File Offset: 0x000025F4
		internal static SafeCertContextHandle InvalidHandle
		{
			get
			{
				SafeCertContextHandle safeCertContextHandle = new SafeCertContextHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertContextHandle);
				return safeCertContextHandle;
			}
		}

		// Token: 0x0600008A RID: 138
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern bool CertFreeCertificateContext(IntPtr pCertContext);

		// Token: 0x0600008B RID: 139 RVA: 0x00004413 File Offset: 0x00002613
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeCertContextHandle.CertFreeCertificateContext(this.handle);
		}
	}
}
