using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A45 RID: 2629
	[SuppressUnmanagedCodeSecurity]
	internal class CertificateHandle : SafeHandle
	{
		// Token: 0x06006811 RID: 26641
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("Crypt32.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern bool CertFreeCertificateContext(IntPtr pCertContext);

		// Token: 0x06006812 RID: 26642
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("Crypt32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		private static extern bool CertDeleteCertificateFromStore(IntPtr pCertContext);

		// Token: 0x06006813 RID: 26643 RVA: 0x001847C9 File Offset: 0x001829C9
		protected CertificateHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x06006814 RID: 26644 RVA: 0x001847D7 File Offset: 0x001829D7
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06006815 RID: 26645 RVA: 0x001847E9 File Offset: 0x001829E9
		protected override bool ReleaseHandle()
		{
			if (this.delete)
			{
				return CertificateHandle.CertDeleteCertificateFromStore(this.handle);
			}
			return CertificateHandle.CertFreeCertificateContext(this.handle);
		}

		// Token: 0x04003BB6 RID: 15286
		protected bool delete;
	}
}
