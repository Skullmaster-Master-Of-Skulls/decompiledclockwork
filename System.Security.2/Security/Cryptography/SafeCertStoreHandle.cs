using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x02000013 RID: 19
	[SecurityCritical]
	internal sealed class SafeCertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600008C RID: 140 RVA: 0x000042C2 File Offset: 0x000024C2
		private SafeCertStoreHandle() : base(true)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000042D8 File Offset: 0x000024D8
		internal SafeCertStoreHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004420 File Offset: 0x00002620
		internal static SafeCertStoreHandle InvalidHandle
		{
			get
			{
				SafeCertStoreHandle safeCertStoreHandle = new SafeCertStoreHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertStoreHandle);
				return safeCertStoreHandle;
			}
		}

		// Token: 0x0600008F RID: 143
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern bool CertCloseStore(IntPtr hCertStore, uint dwFlags);

		// Token: 0x06000090 RID: 144 RVA: 0x0000443F File Offset: 0x0000263F
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeCertStoreHandle.CertCloseStore(this.handle, 0U);
		}
	}
}
