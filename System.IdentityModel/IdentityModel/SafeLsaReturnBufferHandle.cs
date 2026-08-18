using System;
using Microsoft.Win32.SafeHandles;

namespace System.IdentityModel
{
	// Token: 0x0200009C RID: 156
	internal sealed class SafeLsaReturnBufferHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x00006319 File Offset: 0x00004519
		private SafeLsaReturnBufferHandle() : base(true)
		{
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00006322 File Offset: 0x00004522
		internal SafeLsaReturnBufferHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00013127 File Offset: 0x00011327
		internal static SafeLsaReturnBufferHandle InvalidHandle
		{
			get
			{
				return new SafeLsaReturnBufferHandle(IntPtr.Zero);
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00013133 File Offset: 0x00011333
		protected override bool ReleaseHandle()
		{
			return NativeMethods.LsaFreeReturnBuffer(this.handle) >= 0;
		}
	}
}
