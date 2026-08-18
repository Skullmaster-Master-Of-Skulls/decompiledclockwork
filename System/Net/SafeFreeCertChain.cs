using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x0200051B RID: 1307
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeCertChain : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600284B RID: 10315 RVA: 0x000A5EB6 File Offset: 0x000A4EB6
		internal SafeFreeCertChain(IntPtr handle) : base(false)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000A5EC8 File Offset: 0x000A4EC8
		public override string ToString()
		{
			return "0x" + base.DangerousGetHandle().ToString("x");
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000A5EF2 File Offset: 0x000A4EF2
		protected override bool ReleaseHandle()
		{
			UnsafeNclNativeMethods.SafeNetHandles.CertFreeCertificateChain(this.handle);
			return true;
		}

		// Token: 0x0400277E RID: 10110
		private const string CRYPT32 = "crypt32.dll";
	}
}
