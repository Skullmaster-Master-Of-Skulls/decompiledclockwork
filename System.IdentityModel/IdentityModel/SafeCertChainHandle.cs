using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.IdentityModel
{
	// Token: 0x02000030 RID: 48
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal class SafeCertChainHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00006319 File Offset: 0x00004519
		private SafeCertChainHandle() : base(true)
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006322 File Offset: 0x00004522
		private SafeCertChainHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006365 File Offset: 0x00004565
		internal static SafeCertChainHandle InvalidHandle
		{
			get
			{
				return new SafeCertChainHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006371 File Offset: 0x00004571
		protected override bool ReleaseHandle()
		{
			CAPI.CertFreeCertificateChain(this.handle);
			return true;
		}
	}
}
