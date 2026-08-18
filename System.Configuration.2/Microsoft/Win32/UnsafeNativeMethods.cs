using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.Win32
{
	// Token: 0x02000008 RID: 8
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06000009 RID: 9
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool GetFileAttributesEx(string name, int fileInfoLevel, out UnsafeNativeMethods.WIN32_FILE_ATTRIBUTE_DATA data);

		// Token: 0x0600000A RID: 10
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int GetModuleFileName(HandleRef hModule, StringBuilder buffer, int length);

		// Token: 0x0600000B RID: 11
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool CryptProtectData(ref DATA_BLOB inputData, string description, ref DATA_BLOB entropy, IntPtr pReserved, ref CRYPTPROTECT_PROMPTSTRUCT promptStruct, uint flags, ref DATA_BLOB outputData);

		// Token: 0x0600000C RID: 12
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool CryptUnprotectData(ref DATA_BLOB inputData, IntPtr description, ref DATA_BLOB entropy, IntPtr pReserved, ref CRYPTPROTECT_PROMPTSTRUCT promptStruct, uint flags, ref DATA_BLOB outputData);

		// Token: 0x0600000D RID: 13
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int CryptAcquireContext(out SafeCryptContextHandle phProv, string pszContainer, string pszProvider, uint dwProvType, uint dwFlags);

		// Token: 0x0600000E RID: 14
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int CryptReleaseContext(SafeCryptContextHandle hProv, uint dwFlags);

		// Token: 0x0600000F RID: 15
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr LocalFree(IntPtr buf);

		// Token: 0x06000010 RID: 16
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, int dwFlags);

		// Token: 0x04000051 RID: 81
		internal const int GetFileExInfoStandard = 0;

		// Token: 0x04000052 RID: 82
		internal const int MOVEFILE_REPLACE_EXISTING = 1;

		// Token: 0x020000C5 RID: 197
		internal struct WIN32_FILE_ATTRIBUTE_DATA
		{
			// Token: 0x0400046F RID: 1135
			internal int fileAttributes;

			// Token: 0x04000470 RID: 1136
			internal uint ftCreationTimeLow;

			// Token: 0x04000471 RID: 1137
			internal uint ftCreationTimeHigh;

			// Token: 0x04000472 RID: 1138
			internal uint ftLastAccessTimeLow;

			// Token: 0x04000473 RID: 1139
			internal uint ftLastAccessTimeHigh;

			// Token: 0x04000474 RID: 1140
			internal uint ftLastWriteTimeLow;

			// Token: 0x04000475 RID: 1141
			internal uint ftLastWriteTimeHigh;

			// Token: 0x04000476 RID: 1142
			internal uint fileSizeHigh;

			// Token: 0x04000477 RID: 1143
			internal uint fileSizeLow;
		}
	}
}
