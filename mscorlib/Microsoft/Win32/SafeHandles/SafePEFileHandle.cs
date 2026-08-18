using System;
using System.Security.Policy;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200047B RID: 1147
	internal sealed class SafePEFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002DAB RID: 11691 RVA: 0x00098F47 File Offset: 0x00097F47
		private SafePEFileHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x00098F57 File Offset: 0x00097F57
		internal static SafePEFileHandle InvalidHandle
		{
			get
			{
				return new SafePEFileHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x00098F63 File Offset: 0x00097F63
		protected override bool ReleaseHandle()
		{
			Hash._ReleasePEFile(this.handle);
			return true;
		}
	}
}
