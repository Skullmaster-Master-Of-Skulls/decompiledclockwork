using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32
{
	// Token: 0x02000005 RID: 5
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06000001 RID: 1
		[SecurityCritical]
		[DllImport("kernel32.dll", EntryPoint = "GetCurrentPackageId")]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern int _GetCurrentPackageId(ref int pBufferLength, byte[] pBuffer);

		// Token: 0x06000002 RID: 2
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string methodName);

		// Token: 0x06000003 RID: 3
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string moduleName);

		// Token: 0x06000004 RID: 4 RVA: 0x00002050 File Offset: 0x00000250
		[SecurityCritical]
		private static bool DoesWin32MethodExist(string moduleName, string methodName)
		{
			IntPtr moduleHandle = UnsafeNativeMethods.GetModuleHandle(moduleName);
			if (moduleHandle == IntPtr.Zero)
			{
				return false;
			}
			IntPtr procAddress = UnsafeNativeMethods.GetProcAddress(moduleHandle, methodName);
			return procAddress != IntPtr.Zero;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002088 File Offset: 0x00000288
		[SecuritySafeCritical]
		private static bool _IsPackagedProcess()
		{
			OperatingSystem osversion = Environment.OSVersion;
			if (osversion.Platform == PlatformID.Win32NT && osversion.Version >= new Version(6, 2, 0, 0) && UnsafeNativeMethods.DoesWin32MethodExist("kernel32.dll", "GetCurrentPackageId"))
			{
				int num = 0;
				return UnsafeNativeMethods._GetCurrentPackageId(ref num, null) == 122;
			}
			return false;
		}

		// Token: 0x04000050 RID: 80
		internal const string KERNEL32 = "kernel32.dll";

		// Token: 0x04000051 RID: 81
		internal const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x04000052 RID: 82
		internal const int ERROR_NO_PACKAGE_IDENTITY = 15700;

		// Token: 0x04000053 RID: 83
		[SecuritySafeCritical]
		internal static Lazy<bool> IsPackagedProcess = new Lazy<bool>(() => UnsafeNativeMethods._IsPackagedProcess());
	}
}
