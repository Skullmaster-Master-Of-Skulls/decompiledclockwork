using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F6 RID: 502
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeCertChainList : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600131E RID: 4894 RVA: 0x0006468C File Offset: 0x0006288C
		internal SafeFreeCertChainList() : base(true)
		{
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00064698 File Offset: 0x00062898
		public override string ToString()
		{
			return "0x" + base.DangerousGetHandle().ToString("x");
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000646C2 File Offset: 0x000628C2
		protected override bool ReleaseHandle()
		{
			UnsafeNclNativeMethods.SafeNetHandles.CertFreeCertificateChainList(this.handle);
			return true;
		}
	}
}
