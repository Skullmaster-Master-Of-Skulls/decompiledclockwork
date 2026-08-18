using System;
using Microsoft.Win32.SafeHandles;

namespace System.IdentityModel
{
	// Token: 0x0200006F RID: 111
	internal class SafeProvHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600035F RID: 863 RVA: 0x00006319 File Offset: 0x00004519
		private SafeProvHandle() : base(true)
		{
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00006322 File Offset: 0x00004522
		private SafeProvHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000D5A2 File Offset: 0x0000B7A2
		internal static SafeProvHandle InvalidHandle
		{
			get
			{
				return new SafeProvHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000D5AE File Offset: 0x0000B7AE
		protected override bool ReleaseHandle()
		{
			return NativeMethods.CryptReleaseContext(this.handle, 0U);
		}
	}
}
