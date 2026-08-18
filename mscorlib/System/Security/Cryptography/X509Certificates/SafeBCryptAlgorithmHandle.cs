using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008D7 RID: 2263
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeBCryptAlgorithmHandle : SafeHandle
	{
		// Token: 0x06005274 RID: 21108
		[DllImport("bcrypt.dll")]
		private static extern int BCryptCloseAlgorithmProvider([In] IntPtr hAlgorithm, [In] uint dwFlags);

		// Token: 0x06005275 RID: 21109 RVA: 0x00128B63 File Offset: 0x00127B63
		public SafeBCryptAlgorithmHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06005276 RID: 21110 RVA: 0x00128B71 File Offset: 0x00127B71
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x00128B84 File Offset: 0x00127B84
		protected sealed override bool ReleaseHandle()
		{
			int num = SafeBCryptAlgorithmHandle.BCryptCloseAlgorithmProvider(this.handle, 0U);
			return num == 0;
		}
	}
}
