using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F5 RID: 501
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeCertChain : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600131A RID: 4890 RVA: 0x00064631 File Offset: 0x00062831
		internal SafeFreeCertChain(IntPtr handle) : base(false)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00064641 File Offset: 0x00062841
		internal SafeFreeCertChain(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00064654 File Offset: 0x00062854
		public override string ToString()
		{
			return "0x" + base.DangerousGetHandle().ToString("x");
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0006467E File Offset: 0x0006287E
		protected override bool ReleaseHandle()
		{
			UnsafeNclNativeMethods.SafeNetHandles.CertFreeCertificateChain(this.handle);
			return true;
		}
	}
}
