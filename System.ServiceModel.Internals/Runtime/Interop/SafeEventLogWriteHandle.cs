using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Runtime.Interop
{
	// Token: 0x0200003B RID: 59
	[SecurityCritical]
	internal sealed class SafeEventLogWriteHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000944B File Offset: 0x0000764B
		[SecurityCritical]
		private SafeEventLogWriteHandle() : base(true)
		{
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009454 File Offset: 0x00007654
		[SecurityCritical]
		public static SafeEventLogWriteHandle RegisterEventSource(string uncServerName, string sourceName)
		{
			SafeEventLogWriteHandle safeEventLogWriteHandle = UnsafeNativeMethods.RegisterEventSource(uncServerName, sourceName);
			int lastWin32Error = Marshal.GetLastWin32Error();
			bool isInvalid = safeEventLogWriteHandle.IsInvalid;
			return safeEventLogWriteHandle;
		}

		// Token: 0x0600023F RID: 575
		[DllImport("advapi32", SetLastError = true)]
		private static extern bool DeregisterEventSource(IntPtr hEventLog);

		// Token: 0x06000240 RID: 576 RVA: 0x00009477 File Offset: 0x00007677
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeEventLogWriteHandle.DeregisterEventSource(this.handle);
		}
	}
}
