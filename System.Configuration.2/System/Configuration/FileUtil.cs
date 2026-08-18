using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System.Configuration
{
	// Token: 0x02000060 RID: 96
	internal static class FileUtil
	{
		// Token: 0x060003CE RID: 974 RVA: 0x00013D6C File Offset: 0x00011F6C
		internal static bool FileExists(string filename, bool trueOnError)
		{
			UnsafeNativeMethods.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA;
			bool fileAttributesEx = UnsafeNativeMethods.GetFileAttributesEx(filename, 0, out win32_FILE_ATTRIBUTE_DATA);
			if (fileAttributesEx)
			{
				return (win32_FILE_ATTRIBUTE_DATA.fileAttributes & 16) != 16;
			}
			if (!trueOnError)
			{
				return false;
			}
			int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
			return hrforLastWin32Error != -2147024894 && hrforLastWin32Error != -2147024893;
		}

		// Token: 0x0400027F RID: 639
		private const int HRESULT_WIN32_FILE_NOT_FOUND = -2147024894;

		// Token: 0x04000280 RID: 640
		private const int HRESULT_WIN32_PATH_NOT_FOUND = -2147024893;
	}
}
