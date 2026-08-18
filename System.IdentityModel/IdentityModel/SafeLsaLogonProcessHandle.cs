using System;
using Microsoft.Win32.SafeHandles;

namespace System.IdentityModel
{
	// Token: 0x0200009B RID: 155
	internal sealed class SafeLsaLogonProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x00006319 File Offset: 0x00004519
		private SafeLsaLogonProcessHandle() : base(true)
		{
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00006322 File Offset: 0x00004522
		internal SafeLsaLogonProcessHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00013108 File Offset: 0x00011308
		internal static SafeLsaLogonProcessHandle InvalidHandle
		{
			get
			{
				return new SafeLsaLogonProcessHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00013114 File Offset: 0x00011314
		protected override bool ReleaseHandle()
		{
			return NativeMethods.LsaDeregisterLogonProcess(this.handle) >= 0;
		}
	}
}
