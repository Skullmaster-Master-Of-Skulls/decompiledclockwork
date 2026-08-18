using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002EF RID: 751
	[SuppressUnmanagedCodeSecurity]
	internal class SafeCancelMibChangeNotify : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001A6A RID: 6762 RVA: 0x00080144 File Offset: 0x0007E344
		public SafeCancelMibChangeNotify() : base(true)
		{
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00080150 File Offset: 0x0007E350
		protected override bool ReleaseHandle()
		{
			uint num = UnsafeNetInfoNativeMethods.CancelMibChangeNotify2(this.handle);
			this.handle = IntPtr.Zero;
			return num == 0U;
		}
	}
}
