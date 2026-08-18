using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.IdentityModel
{
	// Token: 0x0200002F RID: 47
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal class SafeCertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00006319 File Offset: 0x00004519
		private SafeCertContextHandle() : base(true)
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006322 File Offset: 0x00004522
		private SafeCertContextHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000634C File Offset: 0x0000454C
		internal static SafeCertContextHandle InvalidHandle
		{
			get
			{
				return new SafeCertContextHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006358 File Offset: 0x00004558
		protected override bool ReleaseHandle()
		{
			return CAPI.CertFreeCertificateContext(this.handle);
		}
	}
}
