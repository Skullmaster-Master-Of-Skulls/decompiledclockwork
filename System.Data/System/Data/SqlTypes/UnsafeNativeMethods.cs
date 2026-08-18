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
	// Token: 0x0200037A RID: 890
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06002F64 RID: 12132
		[DllImport("NtDll.dll", CharSet = CharSet.Unicode)]
		internal static extern uint NtCreateFile(out SafeFileHandle fileHandle, int desiredAccess, ref UnsafeNativeMethods.OBJECT_ATTRIBUTES objectAttributes, out UnsafeNativeMethods.IO_STATUS_BLOCK ioStatusBlock, ref long allocationSize, uint fileAttributes, FileShare shareAccess, uint createDisposition, uint createOptions, SafeHandle eaBuffer, uint eaLength);

		// Token: 0x06002F65 RID: 12133
		[DllImport("Kernel32.dll", SetLastError = true)]
		internal static extern UnsafeNativeMethods.FileType GetFileType(SafeFileHandle hFile);

		// Token: 0x06002F66 RID: 12134
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int GetFullPathName(string path, int numBufferChars, StringBuilder buffer, IntPtr lpFilePartOrNull);

		// Token: 0x06002F67 RID: 12135 RVA: 0x002D47F8 File Offset: 0x002D3BF8
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

		// Token: 0x06002F68 RID: 12136
		[DllImport("Kernel32.dll")]
		internal static extern uint SetErrorMode(uint mode);

		// Token: 0x06002F69 RID: 12137
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool DeviceIoControl(SafeFileHandle fileHandle, uint ioControlCode, IntPtr inBuffer, uint cbInBuffer, IntPtr outBuffer, uint cbOutBuffer, out uint cbBytesReturned, IntPtr overlapped);

		// Token: 0x06002F6A RID: 12138
		[DllImport("NtDll.dll")]
		internal static extern uint RtlNtStatusToDosError(uint status);

		// Token: 0x06002F6B RID: 12139 RVA: 0x002D4888 File Offset: 0x002D3C88
		internal static uint CTL_CODE(ushort deviceType, ushort function, byte method, byte access)
		{
			if (function > 4095)
			{
				throw ADP.ArgumentOutOfRange("function");
			}
			return (uint)((int)deviceType << 16 | (int)access << 14 | (int)function << 2 | (int)method);
		}

		// Token: 0x04001D6E RID: 7534
		internal const ushort FILE_DEVICE_FILE_SYSTEM = 9;

		// Token: 0x04001D6F RID: 7535
		internal const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x04001D70 RID: 7536
		internal const int ERROR_MR_MID_NOT_FOUND = 317;

		// Token: 0x04001D71 RID: 7537
		internal const uint STATUS_INVALID_PARAMETER = 3221225485U;

		// Token: 0x04001D72 RID: 7538
		internal const uint STATUS_SHARING_VIOLATION = 3221225539U;

		// Token: 0x04001D73 RID: 7539
		internal const uint STATUS_OBJECT_NAME_NOT_FOUND = 3221225524U;

		// Token: 0x04001D74 RID: 7540
		internal const uint SEM_FAILCRITICALERRORS = 1U;

		// Token: 0x04001D75 RID: 7541
		internal const int FILE_READ_DATA = 1;

		// Token: 0x04001D76 RID: 7542
		internal const int FILE_WRITE_DATA = 2;

		// Token: 0x04001D77 RID: 7543
		internal const int FILE_READ_ATTRIBUTES = 128;

		// Token: 0x04001D78 RID: 7544
		internal const int SYNCHRONIZE = 1048576;

		// Token: 0x0200037B RID: 891
		internal enum FileType : uint
		{
			// Token: 0x04001D7A RID: 7546
			Unknown,
			// Token: 0x04001D7B RID: 7547
			Disk,
			// Token: 0x04001D7C RID: 7548
			Char,
			// Token: 0x04001D7D RID: 7549
			Pipe,
			// Token: 0x04001D7E RID: 7550
			Remote = 32768U
		}

		// Token: 0x0200037C RID: 892
		internal struct OBJECT_ATTRIBUTES
		{
			// Token: 0x04001D7F RID: 7551
			internal int length;

			// Token: 0x04001D80 RID: 7552
			internal IntPtr rootDirectory;

			// Token: 0x04001D81 RID: 7553
			internal SafeHandle objectName;

			// Token: 0x04001D82 RID: 7554
			internal int attributes;

			// Token: 0x04001D83 RID: 7555
			internal IntPtr securityDescriptor;

			// Token: 0x04001D84 RID: 7556
			internal SafeHandle securityQualityOfService;
		}

		// Token: 0x0200037D RID: 893
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct UNICODE_STRING
		{
			// Token: 0x04001D85 RID: 7557
			internal ushort length;

			// Token: 0x04001D86 RID: 7558
			internal ushort maximumLength;

			// Token: 0x04001D87 RID: 7559
			internal string buffer;
		}

		// Token: 0x0200037E RID: 894
		internal enum SecurityImpersonationLevel
		{
			// Token: 0x04001D89 RID: 7561
			SecurityAnonymous,
			// Token: 0x04001D8A RID: 7562
			SecurityIdentification,
			// Token: 0x04001D8B RID: 7563
			SecurityImpersonation,
			// Token: 0x04001D8C RID: 7564
			SecurityDelegation
		}

		// Token: 0x0200037F RID: 895
		internal struct SECURITY_QUALITY_OF_SERVICE
		{
			// Token: 0x04001D8D RID: 7565
			internal uint length;

			// Token: 0x04001D8E RID: 7566
			[MarshalAs(UnmanagedType.I4)]
			internal int impersonationLevel;

			// Token: 0x04001D8F RID: 7567
			internal byte contextDynamicTrackingMode;

			// Token: 0x04001D90 RID: 7568
			internal byte effectiveOnly;
		}

		// Token: 0x02000380 RID: 896
		internal struct IO_STATUS_BLOCK
		{
			// Token: 0x04001D91 RID: 7569
			internal uint status;

			// Token: 0x04001D92 RID: 7570
			internal IntPtr information;
		}

		// Token: 0x02000381 RID: 897
		internal struct FILE_FULL_EA_INFORMATION
		{
			// Token: 0x04001D93 RID: 7571
			internal uint nextEntryOffset;

			// Token: 0x04001D94 RID: 7572
			internal byte flags;

			// Token: 0x04001D95 RID: 7573
			internal byte EaNameLength;

			// Token: 0x04001D96 RID: 7574
			internal ushort EaValueLength;

			// Token: 0x04001D97 RID: 7575
			internal byte EaName;
		}
	}
}
