using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x02000502 RID: 1282
	internal class ProcessWaitHandle : WaitHandle
	{
		// Token: 0x060030BC RID: 12476 RVA: 0x000DC398 File Offset: 0x000DA598
		internal ProcessWaitHandle(SafeProcessHandle processHandle)
		{
			SafeWaitHandle safeWaitHandle = null;
			if (!NativeMethods.DuplicateHandle(new HandleRef(this, NativeMethods.GetCurrentProcess()), processHandle, new HandleRef(this, NativeMethods.GetCurrentProcess()), out safeWaitHandle, 0, false, 2))
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			base.SafeWaitHandle = safeWaitHandle;
		}
	}
}
