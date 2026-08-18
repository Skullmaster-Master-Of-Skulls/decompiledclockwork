using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Web.Hosting;
using Microsoft.Win32.SafeHandles;

namespace System.Web.Util
{
	// Token: 0x020001CB RID: 459
	internal sealed class StopListeningWaitHandle : WaitHandle
	{
		// Token: 0x06001760 RID: 5984 RVA: 0x000496BC File Offset: 0x000478BC
		public StopListeningWaitHandle()
		{
			IntPtr hSourceHandle = UnsafeIISMethods.MgdGetStopListeningEventHandle();
			SafeWaitHandle safeWaitHandle;
			if (!StopListeningWaitHandle.DuplicateHandle(StopListeningWaitHandle._processHandle, hSourceHandle, StopListeningWaitHandle._processHandle, out safeWaitHandle, 0U, false, 2U))
			{
				int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
				Marshal.ThrowExceptionForHR(hrforLastWin32Error);
			}
			base.SafeWaitHandle = safeWaitHandle;
		}

		// Token: 0x06001761 RID: 5985
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetCurrentProcess();

		// Token: 0x06001762 RID: 5986
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool DuplicateHandle([In] IntPtr hSourceProcessHandle, [In] IntPtr hSourceHandle, [In] IntPtr hTargetProcessHandle, out SafeWaitHandle lpTargetHandle, [In] uint dwDesiredAccess, [In] bool bInheritHandle, [In] uint dwOptions);

		// Token: 0x04001707 RID: 5895
		private static IntPtr _processHandle = StopListeningWaitHandle.GetCurrentProcess();
	}
}
