using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200045A RID: 1114
	internal sealed class SafeCertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600297D RID: 10621 RVA: 0x000BC9ED File Offset: 0x000BABED
		private SafeCertContextHandle() : base(true)
		{
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x000BC9F6 File Offset: 0x000BABF6
		internal SafeCertContextHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x000BCA08 File Offset: 0x000BAC08
		internal static SafeCertContextHandle InvalidHandle
		{
			get
			{
				SafeCertContextHandle safeCertContextHandle = new SafeCertContextHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertContextHandle);
				return safeCertContextHandle;
			}
		}

		// Token: 0x06002980 RID: 10624
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern bool CertFreeCertificateContext(IntPtr pCertContext);

		// Token: 0x06002981 RID: 10625 RVA: 0x000BCA27 File Offset: 0x000BAC27
		protected override bool ReleaseHandle()
		{
			return SafeCertContextHandle.CertFreeCertificateContext(this.handle);
		}
	}
}
