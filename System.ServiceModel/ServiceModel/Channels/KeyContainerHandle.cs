using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A48 RID: 2632
	[SuppressUnmanagedCodeSecurity]
	internal sealed class KeyContainerHandle : SafeHandle
	{
		// Token: 0x0600681B RID: 26651
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern bool CryptReleaseContext(IntPtr hProv, int dwFlags);

		// Token: 0x0600681C RID: 26652 RVA: 0x00184847 File Offset: 0x00182A47
		private KeyContainerHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x0600681D RID: 26653 RVA: 0x00184855 File Offset: 0x00182A55
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x0600681E RID: 26654 RVA: 0x00184867 File Offset: 0x00182A67
		protected override bool ReleaseHandle()
		{
			return KeyContainerHandle.CryptReleaseContext(this.handle, 0);
		}
	}
}
