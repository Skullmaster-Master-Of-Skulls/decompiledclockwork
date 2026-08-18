using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F0 RID: 752
	[SuppressUnmanagedCodeSecurity]
	internal class SafeFreeMibTable : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001A6C RID: 6764 RVA: 0x00080178 File Offset: 0x0007E378
		public SafeFreeMibTable() : base(true)
		{
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00080181 File Offset: 0x0007E381
		protected override bool ReleaseHandle()
		{
			UnsafeNetInfoNativeMethods.FreeMibTable(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}
	}
}
