using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A47 RID: 2631
	[SuppressUnmanagedCodeSecurity]
	internal sealed class CertificateStoreHandle : SafeHandle
	{
		// Token: 0x06006817 RID: 26647
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("Crypt32.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern bool CertCloseStore(IntPtr hCertStore, int dwFlags);

		// Token: 0x06006818 RID: 26648 RVA: 0x00184819 File Offset: 0x00182A19
		private CertificateStoreHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x06006819 RID: 26649 RVA: 0x00184827 File Offset: 0x00182A27
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x0600681A RID: 26650 RVA: 0x00184839 File Offset: 0x00182A39
		protected override bool ReleaseHandle()
		{
			return CertificateStoreHandle.CertCloseStore(this.handle, 0);
		}
	}
}
