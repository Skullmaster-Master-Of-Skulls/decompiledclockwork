using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200045B RID: 1115
	internal sealed class SafeCertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002982 RID: 10626 RVA: 0x000BCA34 File Offset: 0x000BAC34
		private SafeCertStoreHandle() : base(true)
		{
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000BCA3D File Offset: 0x000BAC3D
		internal SafeCertStoreHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x000BCA50 File Offset: 0x000BAC50
		internal static SafeCertStoreHandle InvalidHandle
		{
			get
			{
				SafeCertStoreHandle safeCertStoreHandle = new SafeCertStoreHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertStoreHandle);
				return safeCertStoreHandle;
			}
		}

		// Token: 0x06002985 RID: 10629
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern bool CertCloseStore(IntPtr hCertStore, uint dwFlags);

		// Token: 0x06002986 RID: 10630 RVA: 0x000BCA6F File Offset: 0x000BAC6F
		protected override bool ReleaseHandle()
		{
			return SafeCertStoreHandle.CertCloseStore(this.handle, 0U);
		}
	}
}
