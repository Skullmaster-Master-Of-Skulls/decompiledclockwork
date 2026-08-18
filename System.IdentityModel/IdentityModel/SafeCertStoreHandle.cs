using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.IdentityModel
{
	// Token: 0x0200002E RID: 46
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal class SafeCertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00006319 File Offset: 0x00004519
		private SafeCertStoreHandle() : base(true)
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006322 File Offset: 0x00004522
		private SafeCertStoreHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00006332 File Offset: 0x00004532
		public static SafeCertStoreHandle InvalidHandle
		{
			get
			{
				return new SafeCertStoreHandle(IntPtr.Zero);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000633E File Offset: 0x0000453E
		protected override bool ReleaseHandle()
		{
			return CAPI.CertCloseStore(this.handle, 0U);
		}
	}
}
