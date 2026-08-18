using System;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace System.Data.SqlTypes
{
	// Token: 0x0200014F RID: 335
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x0600136F RID: 4975
		[DllImport("NtDll.dll", CharSet = CharSet.Unicode)]
		internal static extern uint NtCreateFile(out SafeFileHandle fileHandle, int desiredAccess, ref UnsafeNativeMethods.OBJECT_ATTRIBUTES objectAttributes, out UnsafeNativeMethods.IO_STATUS_BLOCK ioStatusBlock, ref long allocationSize, uint fileAttributes, FileShare shareAccess, uint createDisposition, uint createOptions, SafeHandle eaBuffer, uint eaLength);

		// Token: 0x06001370 RID: 4976
		[DllImport("Kernel32.dll", SetLastError = true)]
		internal static extern UnsafeNativeMethods.FileType GetFileType(SafeFileHandle hFile);

		// Token: 0x06001371 RID: 4977
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int GetFullPathName(string path, int numBufferChars, StringBuilder buffer, IntPtr lpFilePartOrNull);

		// Token: 0x06001372 RID: 4978 RVA: 0x0009A66C File Offset: 0x00099A6C
		internal static string SafeGetFullPathName(string path)
		{
			StringBuilder stringBuilder = new StringBuilder(path.Length + 1);
			int fullPathName = UnsafeNativeMethods.GetFullPathName(path, stringBuilder.Capacity, stringBuilder, IntPtr.Zero);
			if (fullPathName > stringBuilder.Capacity)
			{
				stringBuilder.Capacity = fullPathName;
				fullPathName = UnsafeNativeMethods.GetFullPathName(path, stringBuilder.Capacity, stringBuilder, IntPtr.Zero);
			}
			if (fullPathName != 0)
			{
				return stringBuilder.ToString();
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 0)
			{
				throw ADP.Argument(Res.GetString("SqlFileStream_InvalidPath"), "path");
			}
			Win32Exception ex = new Win32Exception(lastWin32Error);
			ADP.TraceExceptionAsReturnValue(ex);
			throw ex;
		}

		// Token: 0x06001373 RID: 4979
		[DllImport("Kernel32.dll", ExactSpelling = true)]
		private static extern uint SetErrorMode(uint mode);

		// Token: 0x06001374 RID: 4980
		[DllImport("Kernel32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern bool SetThreadErrorMode(uint newMode, out uint oldMode);

		// Token: 0x06001375 RID: 4981 RVA: 0x0009A6F4 File Offset: 0x00099AF4
		internal static void SetErrorModeWrapper(uint mode, out uint oldMode)
		{
			if (Environment.OSVersion.Version >= UnsafeNativeMethods.ThreadErrorModeMinOsVersion)
			{
				if (!UnsafeNativeMethods.SetThreadErrorMode(mode, out oldMode))
				{
					throw new Win32Exception();
				}
			}
			else
			{
				oldMode = UnsafeNativeMethods.SetErrorMode(mode);
			}
		}

		// Token: 0x06001376 RID: 4982
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool DeviceIoControl(SafeFileHandle fileHandle, uint ioControlCode, IntPtr inBuffer, uint cbInBuffer, IntPtr outBuffer, uint cbOutBuffer, out uint cbBytesReturned, IntPtr overlapped);

		// Token: 0x06001377 RID: 4983
		[DllImport("NtDll.dll")]
		internal static extern uint RtlNtStatusToDosError(uint status);

		// Token: 0x06001378 RID: 4984 RVA: 0x0009A730 File Offset: 0x00099B30
		internal static uint CTL_CODE(ushort deviceType, ushort function, byte method, byte access)
		{
			if (function > 4095)
			{
				throw ADP.ArgumentOutOfRange("function");
			}
			return (uint)((int)deviceType << 16 | (int)access << 14 | (int)function << 2 | (int)method);
		}

		// Token: 0x04000D3E RID: 3390
		private static readonly Version ThreadErrorModeMinOsVersion = new Version(6, 1, 7600);

		// Token: 0x04000D3F RID: 3391
		internal const ushort FILE_DEVICE_FILE_SYSTEM = 9;

		// Token: 0x04000D40 RID: 3392
		internal const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x04000D41 RID: 3393
		internal const int ERROR_MR_MID_NOT_FOUND = 317;

		// Token: 0x04000D42 RID: 3394
		internal const uint STATUS_INVALID_PARAMETER = 3221225485U;

		// Token: 0x04000D43 RID: 3395
		internal const uint STATUS_SHARING_VIOLATION = 3221225539U;

		// Token: 0x04000D44 RID: 3396
		internal const uint STATUS_OBJECT_NAME_NOT_FOUND = 3221225524U;

		// Token: 0x04000D45 RID: 3397
		internal const uint SEM_FAILCRITICALERRORS = 1U;

		// Token: 0x04000D46 RID: 3398
		internal const int FILE_READ_DATA = 1;

		// Token: 0x04000D47 RID: 3399
		internal const int FILE_WRITE_DATA = 2;

		// Token: 0x04000D48 RID: 3400
		internal const int FILE_READ_ATTRIBUTES = 128;

		// Token: 0x04000D49 RID: 3401
		internal const int SYNCHRONIZE = 1048576;

		// Token: 0x0200036B RID: 875
		internal enum FileType : uint
		{
			// Token: 0x04001F1A RID: 7962
			Unknown,
			// Token: 0x04001F1B RID: 7963
			Disk,
			// Token: 0x04001F1C RID: 7964
			Char,
			// Token: 0x04001F1D RID: 7965
			Pipe,
			// Token: 0x04001F1E RID: 7966
			Remote = 32768U
		}

		// Token: 0x0200036C RID: 876
		internal struct OBJECT_ATTRIBUTES
		{
			// Token: 0x04001F1F RID: 7967
			internal int length;

			// Token: 0x04001F20 RID: 7968
			internal IntPtr rootDirectory;

			// Token: 0x04001F21 RID: 7969
			internal SafeHandle objectName;

			// Token: 0x04001F22 RID: 7970
			internal int attributes;

			// Token: 0x04001F23 RID: 7971
			internal IntPtr securityDescriptor;

			// Token: 0x04001F24 RID: 7972
			internal SafeHandle securityQualityOfService;
		}

		// Token: 0x0200036D RID: 877
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct UNICODE_STRING
		{
			// Token: 0x04001F25 RID: 7973
			internal ushort length;

			// Token: 0x04001F26 RID: 7974
			internal ushort maximumLength;

			// Token: 0x04001F27 RID: 7975
			internal string buffer;
		}

		// Token: 0x0200036E RID: 878
		internal enum SecurityImpersonationLevel
		{
			// Token: 0x04001F29 RID: 7977
			SecurityAnonymous,
			// Token: 0x04001F2A RID: 7978
			SecurityIdentification,
			// Token: 0x04001F2B RID: 7979
			SecurityImpersonation,
			// Token: 0x04001F2C RID: 7980
			SecurityDelegation
		}

		// Token: 0x0200036F RID: 879
		internal struct SECURITY_QUALITY_OF_SERVICE
		{
			// Token: 0x04001F2D RID: 7981
			internal uint length;

			// Token: 0x04001F2E RID: 7982
			[MarshalAs(UnmanagedType.I4)]
			internal int impersonationLevel;

			// Token: 0x04001F2F RID: 7983
			internal byte contextDynamicTrackingMode;

			// Token: 0x04001F30 RID: 7984
			internal byte effectiveOnly;
		}

		// Token: 0x02000370 RID: 880
		internal struct IO_STATUS_BLOCK
		{
			// Token: 0x04001F31 RID: 7985
			internal uint status;

			// Token: 0x04001F32 RID: 7986
			internal IntPtr information;
		}

		// Token: 0x02000371 RID: 881
		internal struct FILE_FULL_EA_INFORMATION
		{
			// Token: 0x04001F33 RID: 7987
			internal uint nextEntryOffset;

			// Token: 0x04001F34 RID: 7988
			internal byte flags;

			// Token: 0x04001F35 RID: 7989
			internal byte EaNameLength;

			// Token: 0x04001F36 RID: 7990
			internal ushort EaValueLength;

			// Token: 0x04001F37 RID: 7991
			internal byte EaName;
		}
	}
}
