using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008BC RID: 2236
	internal sealed class SafeCertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600515E RID: 20830 RVA: 0x00123E3A File Offset: 0x00122E3A
		private SafeCertStoreHandle() : base(true)
		{
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x00123E43 File Offset: 0x00122E43
		internal SafeCertStoreHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06005160 RID: 20832 RVA: 0x00123E53 File Offset: 0x00122E53
		internal static SafeCertStoreHandle InvalidHandle
		{
			get
			{
				return new SafeCertStoreHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06005161 RID: 20833
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _FreeCertStoreContext(IntPtr hCertStore);

		// Token: 0x06005162 RID: 20834 RVA: 0x00123E5F File Offset: 0x00122E5F
		protected override bool ReleaseHandle()
		{
			SafeCertStoreHandle._FreeCertStoreContext(this.handle);
			return true;
		}
	}
}
